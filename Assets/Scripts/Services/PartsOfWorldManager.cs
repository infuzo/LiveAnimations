using System.Collections.Generic;
using UnityEngine;
using Runner.Views;

namespace Runner.Services
{
    public interface IPartsOfWorldManager
    {

        bool CreateNextPartOfWorld();

        void RemoveOldestPartOfWorld();
        
    }

    public class PartsOfWorldManager : IPartsOfWorldManager
    {

        class PartsOfWorldManagerWorker : MonoBehaviour
        {

            const byte startPoolSize = GameWorldModel.MaxPartOfWorldCount + 1;

            List<PartOfWorldView> pooledObjects = null;

            private void InitializePool(Controllers.PlayerLeavePartOfWorldSignal playerLeavePartOfWorldSignal)
            {
                if (pooledObjects != null) { return; }

                pooledObjects = new List<PartOfWorldView>();

                for (int nowObjectIndex = 0; nowObjectIndex < startPoolSize; nowObjectIndex++)
                {
                    CreatePooledObject(playerLeavePartOfWorldSignal);
                }
            }

            PartOfWorldView CreatePooledObject(Controllers.PlayerLeavePartOfWorldSignal playerLeavePartOfWorldSignal)
            {
                PartOfWorldView newPart = Instantiate(GameWorldModel.Instance.PartOfWorldPrefab);
                pooledObjects.Add(newPart);
                newPart.gameObject.SetActive(false);
                newPart.InitSignal(playerLeavePartOfWorldSignal);
                return newPart;
            }

            public PartOfWorldView CreateNewPart(Controllers.PlayerLeavePartOfWorldSignal playerLeavePartOfWorldSignal)
            {
                InitializePool(playerLeavePartOfWorldSignal);

                PartOfWorldView newPart = pooledObjects.Find(nowPart => !nowPart.gameObject.activeInHierarchy);

                if (newPart == null)
                {
                    newPart = CreatePooledObject(playerLeavePartOfWorldSignal);
                }

                newPart.gameObject.SetActive(true);
                return newPart;
            }

            public void RemovePart(PartOfWorldView part)
            {
                part.gameObject.SetActive(false);
            }

        }

        [Inject]
        public Controllers.ChangedPartsOfWorldAmountSignal ChangedPartsOfWorldAmountSignal { get; private set; }
        [Inject]
        public Controllers.PlayerLeavePartOfWorldSignal PlayerLeavePartOfWorldSignal { get; private set; }

        PartsOfWorldManagerWorker partsOfWorldManagerWorker;

        public PartsOfWorldManager()
        {
            partsOfWorldManagerWorker = new GameObject("PartsOfWorldManagerWorker").AddComponent<PartsOfWorldManagerWorker>();
        }

        public bool CreateNextPartOfWorld()
        {
            if (GameWorldModel.Instance.PartsOfWorld.Count >= GameWorldModel.MaxPartOfWorldCount) { return false; }

            Vector3 position = GameWorldModel.Instance.PartsOfWorld.Count > 0 ?
                GameWorldModel.Instance.PartsOfWorld.ToArray()[GameWorldModel.Instance.PartsOfWorld.Count - 1].EndPointPosition : Vector3.zero;
            PartOfWorldView newPart = partsOfWorldManagerWorker.CreateNewPart(PlayerLeavePartOfWorldSignal);
            newPart.transform.position = position;
            newPart.transform.rotation = Quaternion.identity;
            GameWorldModel.Instance.PartsOfWorld.Enqueue(newPart as IPartOfWorldView);
            RelinkAllWaypoints();

            return true;
        }

        void RelinkAllWaypoints()
        {
            for (int nowLineIndex = 0; nowLineIndex < GameWorldModel.Instance.AllWaypoints.Length; nowLineIndex++)
            {
                GameWorldModel.Instance.AllWaypoints[nowLineIndex] = new LinkedList<Transform>();
            }

            foreach (IPartOfWorldView nowPartOfWorld in GameWorldModel.Instance.PartsOfWorld)
            {
                int lineCounter = 0;
                foreach (PartOfWorldView.LineInfo nowLineInfo in nowPartOfWorld.LinesInfo)
                {
                    foreach (Transform nowWayPoint in nowLineInfo.WayPoints)
                    {
                        GameWorldModel.Instance.AllWaypoints[lineCounter].AddLast(nowWayPoint);
                    }
                    lineCounter++;
                }
            }
        }

        public void RemoveOldestPartOfWorld()
        {
            IPartOfWorldView oldestPart = GameWorldModel.Instance.PartsOfWorld.Dequeue();
            
            partsOfWorldManagerWorker.RemovePart(oldestPart as PartOfWorldView);
            ChangedPartsOfWorldAmountSignal.Dispatch();
        }

    }
}