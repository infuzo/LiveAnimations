using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Views;
using System.Linq;

namespace Runner
{
    public class GameWorldModel : MonoBehaviour
    {

        public const byte MaxPartOfWorldCount = 3;
        public const byte LinesCount = 3;

        public const string ObstacleTag = "Obstacle";

        public static GameWorldModel Instance { get; private set; }

        /// <summary>
        /// All waypoints of lines.
        /// </summary>
        public Queue<IPartOfWorldView> PartsOfWorld { get; private set; }
        /// <summary>
        /// Sorted list of all waypoints on lines.
        /// </summary>
        public LinkedList<Transform>[] AllWaypoints { get; private set; }
        /// <summary>
        /// Elapsed time in seconds.
        /// </summary>
        public float ElapsedTime { get; set; }

        [Header("Prefabs")]
        [SerializeField]
        PartOfWorldView partOfWorldPrefab;
        [SerializeField]
        GameObject playerPrefab;
        [SerializeField]
        GameObject obstaclePrefab;

        [Header("Speed")]
        [SerializeField]
        float forwardSpeed = 2f;
        [SerializeField]
        float sideSpeed = 2f;

        [Header("Obstacles")]
        [SerializeField]
        float minDistanceBetweenObstacles;
        [SerializeField]
        float maxDistanceBetweenObstacles;
        [SerializeField]
        float sideCheckDistance;

        [Header("Misc")]
        [SerializeField]
        byte startLifesCount = 3;

        private void Awake()
        {
            PartsOfWorld = new Queue<IPartOfWorldView>();
            AllWaypoints = new LinkedList<Transform>[LinesCount];
            Instance = this;
        }

        public PartOfWorldView PartOfWorldPrefab
        {
            get { return partOfWorldPrefab; }
        }

        public GameObject PlayerPrefab
        {
            get { return playerPrefab; }
        }

        public int FindIndexOfWaypoint(int lineIndex, Transform waypoint)
        {
            return AllWaypoints[lineIndex].TakeWhile(nowWaypoint => nowWaypoint != waypoint).Count();
        }

        public Transform GetWaypointByIndex(int lineIndex, int wayPointIndex)
        {
            return AllWaypoints[lineIndex].ToArray()[wayPointIndex];
        }

        public float ForwardSpeed { get { return forwardSpeed; } }

        public float SideSpeed { get { return sideSpeed; } }

        public GameObject ObstaclePrefab { get { return obstaclePrefab; } }

        public float MinDistanceBetweenObstacles { get { return minDistanceBetweenObstacles; } }

        public float MaxDistanceBetweenObstacles { get { return maxDistanceBetweenObstacles; } }

        public float SideCheckDistance { get { return sideCheckDistance; } }

        public byte StartLifesCount { get { return startLifesCount; } }

    }
}