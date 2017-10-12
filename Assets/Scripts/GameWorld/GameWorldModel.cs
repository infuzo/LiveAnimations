using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Views;
using System.Linq;

namespace Runner
{
    public class GameWorldModel : MonoBehaviour
    {

        public const byte MaxPartOfWorldCount = 2;
        public const byte LinesCount = 3;

        public static GameWorldModel Instance { get; private set; }

        /// <summary>
        /// All waypoints of lines.
        /// </summary>
        public Queue<IPartOfWorldView> PartsOfWorld { get; private set; }
        /// <summary>
        /// Sorted list of all waypoints on lines.
        /// </summary>
        public LinkedList<Transform>[] AllWaypoints { get; private set; }

        [SerializeField]
        PartOfWorldView partOfWorldPrefab;
        [SerializeField]
        GameObject playerPrefab;

        [SerializeField]
        float forwardSpeed = 2f;

        [SerializeField]
        float sideSpeed = 2f;

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

    }
}