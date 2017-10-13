using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Views.UI;

namespace Runner.Models.UI
{
    public class GameUIModel : MonoBehaviour
    {

        public static GameUIModel Instance { get; private set; }

        [SerializeField]
        GameObject touchInputManagerPrefab;
        [SerializeField]
        GameObject gameScreenViewWorkerPrefab;
        [SerializeField]
        GameObject pausePopupPrefab;
        [SerializeField]
        GameObject loosePopupPrefab;

        private void Awake()
        {
            Instance = this;
        }

        public GameObject TouchInputManagerPrefab { get { return touchInputManagerPrefab; } }

        public GameObject GameScreenViewWorkerPrefab { get { return gameScreenViewWorkerPrefab; } }

        public GameObject PausePopupPrefab { get { return pausePopupPrefab; } }

        public GameObject LoosePopupPrefab { get { return loosePopupPrefab; } }

    }
}