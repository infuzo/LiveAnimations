using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.World
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

        [SerializeField]
        PartOfWorldView partOfWorldPrefab;

        private void Awake()
        {
            PartsOfWorld = new Queue<IPartOfWorldView>();
            Instance = this;
        }

        public PartOfWorldView PartOfWorldPrefab
        {
            get { return partOfWorldPrefab; }
        }

    }
}