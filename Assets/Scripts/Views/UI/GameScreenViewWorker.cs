using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Runner.Services.UI;

namespace Runner.Views.UI
{
    class GameScreenViewWorker : MonoBehaviour
    {

        [SerializeField]
        Text textTimeElapsed;
        [SerializeField]
        Text textLifesCount;

        public Text TextTimeElapsed { get { return textTimeElapsed; } }

        public Text TextLifesCount { get { return textLifesCount; } }

        public void ButtonPause_OnClick()
        {
            MainContext.InjectionBinder.GetInstance<Controllers.ChangePauseStateSignal>().Dispatch(true);
        }

    }

    public class GameScreenView
    {
        
        GameScreenViewWorker gameScreenViewWorker;

        public GameScreenView()
        {
            gameScreenViewWorker = MainContext.InjectionBinder.GetInstance<IGameUIManager>().
                InstantiateLikeUIElement(Models.UI.GameUIModel.Instance.GameScreenViewWorkerPrefab).GetComponent<GameScreenViewWorker>();
        }

        public void SetElapsedTime(string elapsedTime)
        {
            gameScreenViewWorker.TextTimeElapsed.text = "Elapsed time: " + elapsedTime;
        }

        public void SetLifesCount(int lifesCount)
        {
            gameScreenViewWorker.TextLifesCount.text = "Lifes: " + lifesCount;
        }

    }
}