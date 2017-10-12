using System.Collections.Generic;
using Runner.Views;
using UnityEngine;

namespace Runner.Services
{

    public interface IObstaclesManager
    {

        void CreateObstaclesOnPartOfWorld(IPartOfWorldView targetPart);

        void RemoveAllObstaclesFromPartOfWorld(IPartOfWorldView targetPart);

    }

    public class ObstaclesManager : IObstaclesManager
    {

        class ObstaclesManagerWorker : MonoBehaviour
        {

            const byte poolSize = 5;

            List<GameObject> pooledObject = null;
            List<GameObject> activeObjects = new List<GameObject>();

            void InitializePool()
            {
                if(pooledObject != null) { return; }

                pooledObject = new List<GameObject>();

                for (int nowPoolObjectIndex = 0; nowPoolObjectIndex < poolSize; nowPoolObjectIndex++)
                {
                    CreatePooledObject();
                }
            }

            GameObject CreatePooledObject()
            {
                GameObject newObject = Instantiate(GameWorldModel.Instance.ObstaclePrefab);
                pooledObject.Add(newObject);
                newObject.SetActive(false);
                return newObject;
            }

            public GameObject CreateObjectFromPool()
            {
                InitializePool();

                GameObject newObject = pooledObject.Find(nowObject => !nowObject.activeInHierarchy);
                if(newObject == null)
                {
                    newObject = CreatePooledObject();
                }

                newObject.SetActive(true);
                activeObjects.Add(newObject);
                return newObject;
            }

            public GameObject FindFirstNearObstacle(Vector3 checkedPosition, float maxDistance)
            {
                return activeObjects.Find(nowObstacle => Vector3.Distance(nowObstacle.transform.position, checkedPosition) <= maxDistance);
            }

            public void RemoveObstacle(GameObject obstacle)
            {
                activeObjects.Remove(obstacle);
                obstacle.SetActive(false);
            }

        }

        const float obstaclesYOffset = -.5f;

        ObstaclesManagerWorker obstaclesManagerWorker;

        Dictionary<IPartOfWorldView, List<GameObject>> obstaclesByPart = new Dictionary<IPartOfWorldView, List<GameObject>>();

        public ObstaclesManager()
        {
            obstaclesManagerWorker = new GameObject("ObstaclesManagerWorker").AddComponent<ObstaclesManagerWorker>();
        }

        public void CreateObstaclesOnPartOfWorld(IPartOfWorldView targetPart)
        {
            foreach(PartOfWorldView.LineInfo nowLine in targetPart.LinesInfo)
            {
                CreateObstaclesOnLine(targetPart, nowLine.WayPoints[0].position, nowLine.WayPoints[nowLine.WayPoints.Length - 1].position);
            }
        }

        void CreateObstaclesOnLine(IPartOfWorldView targetPart, Vector3 startPosition, Vector3 endPosition)
        {
            float totalLength = Vector3.Distance(startPosition, endPosition);
            float counter = 0f;
            byte attempsCounter = 0;
            float currentSegment = 0f;
            Vector3 currentPosition;

            while(counter <= 1f)
            {
                attempsCounter = 0;

                while(attempsCounter < 3)
                {
                    currentSegment = Random.Range(GameWorldModel.Instance.MinDistanceBetweenObstacles, GameWorldModel.Instance.MaxDistanceBetweenObstacles) / totalLength;
                    currentPosition = Vector3.Lerp(startPosition, endPosition, counter + currentSegment);
                    if (obstaclesManagerWorker.FindFirstNearObstacle(currentPosition, GameWorldModel.Instance.SideCheckDistance) == null)
                    {
                        Transform newObstacle = obstaclesManagerWorker.CreateObjectFromPool().transform;
                        newObstacle.position = currentPosition + new Vector3(0f, obstaclesYOffset, 0f);
                        AddObstacleToPartOfWorld(targetPart, newObstacle.gameObject);
                        break;
                    }

                    attempsCounter++;
                }

                counter += currentSegment; //Attemp create next obstacle from previous position in any case
            }
        }

        void AddObstacleToPartOfWorld(IPartOfWorldView targetPart, GameObject obstacle)
        {
            if (!obstaclesByPart.ContainsKey(targetPart))
            {
                obstaclesByPart.Add(targetPart, new List<GameObject>());
            }
            obstaclesByPart[targetPart].Add(obstacle);
        }

        public void RemoveAllObstaclesFromPartOfWorld(IPartOfWorldView targetPart)
        {
            if (obstaclesByPart.ContainsKey(targetPart))
            {
                foreach (GameObject nowObstacle in obstaclesByPart[targetPart])
                {
                    obstaclesManagerWorker.RemoveObstacle(nowObstacle);
                }
                obstaclesByPart[targetPart].Clear();
            }
        }

    }

}