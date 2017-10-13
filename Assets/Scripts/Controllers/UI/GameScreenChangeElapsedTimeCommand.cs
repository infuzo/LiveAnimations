using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Runner.Controllers.UI
{

    /// <summary>
    /// Arguments - elapsed time in seconds.
    /// </summary>
    public class GameScreenChangeElapsedTimeSignal : Signal { }

    public class GameScreenChangeElapsedTimeCommand : Command
    {

        [Inject]
        public Views.UI.GameScreenView GameScreenView { get; private set; }

        public override void Execute()
        {
            System.TimeSpan elapsedTime = System.TimeSpan.FromSeconds(GameWorldModel.Instance.ElapsedTime);
            string formated = string.Format("{0:00}:{1:00}", elapsedTime.Minutes, elapsedTime.Seconds);
            GameScreenView.SetElapsedTime(formated);
        }

    }

}