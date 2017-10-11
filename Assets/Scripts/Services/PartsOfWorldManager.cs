using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;
using Runner.World;

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
                if(pooledObjects != null) { return; }

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

                if(newPart == null)
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
            partsOfWorldManagerWorker = new GameObject("PartsOfWorldCreatorWorker").AddComponent<PartsOfWorldManagerWorker>();
        }

        public bool CreateNextPartOfWorld()
        {
            if(GameWorldModel.Instance.PartsOfWorld.Count >= GameWorldModel.MaxPartOfWorldCount) { return false; }

            Vector3 position = GameWorldModel.Instance.PartsOfWorld.Count > 0 ?
                GameWorldModel.Instance.PartsOfWorld.ToArray()[GameWorldModel.Instance.PartsOfWorld.Count - 1].EndPointPosition : Vector3.zero;
            PartOfWorldView newPart = partsOfWorldManagerWorker.CreateNewPart();
            newPart.transform.position = position;
            newPart.transform.rotation = Quaternion.identity;
            GameWorldModel.Instance.PartsOfWorld.Enqueue(newPart as IPartOfWorldView);

            return true;
        }

        public void RemoveOldestPartOfWorld()
        {
            Debug.Log(GameWorldModel.Instance.PartsOfWorld.Count);
            IPartOfWorldView oldestPart = GameWorldModel.Instance.PartsOfWorld.Dequeue();
            partsOfWorldManagerWorker.RemovePart(oldestPart as PartOfWorldView);

            ChangedPartsOfWorldAmountSignal.Dispatch();
        }

    }
}