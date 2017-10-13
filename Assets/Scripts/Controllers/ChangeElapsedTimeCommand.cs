using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Runner.Controllers
{

    public class ChangeElapsedTimeSignal : Signal<float> { }

    public class ChangeElapsedTimeCommand : Command
    {

        /// <summary>
        /// Value for plus to current elapsed time in seconds.
        /// </summary>
        [Inject]
        public float ChangedTime { get; private set; }

        [Inject]
        public UI.GameScreenChangeElapsedTimeSignal GameScreenChangeElapsedTimeSignal { get; private set; }

        public override void Execute()
        {
            GameWorldModel.Instance.ElapsedTime += ChangedTime;
            GameScreenChangeElapsedTimeSignal.Dispatch();
        }

    }

}