using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Services
{

    public interface IGameManager
    {

        void StartGameTimer();

        void StopGameTimer();

    }

    public class GameManager : IGameManager
    {

        class GameManagerWorker : MonoBehaviour
        {

            public bool TimerPause { get; set; }

            private void Update()
            {
                if(TimerPause) { return; }
                MainContext.InjectionBinder.GetInstance<Controllers.ChangeElapsedTimeSignal>().Dispatch(Time.deltaTime);
            }

        }

        GameManagerWorker gameManagerWorker;

        public GameManager()
        {
            gameManagerWorker = new GameObject("GameManagerWorker").AddComponent<GameManagerWorker>();
        }

        public void StartGameTimer()
        {
            gameManagerWorker.TimerPause = false;
        }

        public void StopGameTimer()
        {
            gameManagerWorker.TimerPause = true;
        }

    }

}