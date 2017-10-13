using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Runner.Controllers.UI
{

    public class CreateGameUISignal : Signal { }

    public class CreateGameUICommand : Command
    {

        [Inject]
        public GameScreenChangeElapsedTimeSignal GameScreenChangeElapsedTimeSignal { get; private set; }
        [Inject]
        public GameScreenChangeLifesCountSignal GameScreenChangeLifesCountSignal { get; private set; }

        public override void Execute()
        {
            GameScreenChangeElapsedTimeSignal.Dispatch();
            GameScreenChangeLifesCountSignal.Dispatch();
        }

    }

}