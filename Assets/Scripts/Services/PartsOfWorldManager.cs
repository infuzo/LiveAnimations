using System;
using System.Collections;
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

            const byte startPoolSize = 3;

            List<PartOfWorldView> pooledObjects = null;

            private void InitializePool()
            {
                if (pooledObjects != null) { return; }

                pooledObjects = new List<PartOfWorldView>();

                for (int nowObjectIndex = 0; nowObjectIndex < startPoolSize; nowObjectIndex++)
                {
                    pooledObjects.Add(Instantiate(GameWorldModel.Instance.PartOfWorldPrefab));
                    pooledObjects[nowObjectIndex].gameObject.SetActive(false);
                }
            }

            public PartOfWorldView CreateNewPart()
            {
                InitializePool();

                PartOfWorldView newPart = pooledObjects.Find(nowPart => !nowPart.gameObject.activeInHierarchy);

                if (newPart == null)
                {
                    pooledObjects.Add(Instantiate(GameWorldModel.Instance.PartOfWorldPrefab));
                    newPart = pooledObjects[pooledObjects.Count - 1];
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
            PartOfWorldView newPart = partsOfWorldManagerWorker.CreateNewPart();
            newPart.transform.position = position;
            newPart.transform.rotation = Quaternion.identity;
            GameWorldModel.Instance.PartsOfWorld.Enqueue(newPart as IPartOfWorldView);

            RelinkAllWaypoints();

            return true;
        }

        void RelinkAllWaypoints()
        {
            for(int nowLineIndex = 0; nowLineIndex < GameWorldModel.Instance.AllWaypoints.Length; nowLineIndex++)
            {
                GameWorldModel.Instance.AllWaypoints[nowLineIndex] = new LinkedList<Transform>();
            }
            
            foreach (IPartOfWorldView nowPartOfWorld in GameWorldModel.Instance.PartsOfWorld)
            {
                int lineCounter = 0;
                foreach(PartOfWorldView.LineInfo nowLineInfo in nowPartOfWorld.LinesInfo)
                {
                    foreach(Transform nowWayPoint in nowLineInfo.WayPoints)
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