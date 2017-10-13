using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Runner.Controllers
{

    /// <summary>
    /// Arguments: enable (true) or disable (false) pause.
    /// </summary>
    public class ChangePauseStateSignal : Signal<bool> { }

    public class ChangePauseStateCommand : Command
    {

        [Inject]
        public bool NewPauseState { get; private set; }
        [Inject]
        public UI.PausePopupOpenSignal PausePopupOpenSignal { get; private set; }
        [Inject]
        public UI.PausePopupCloseSignal PausePopupCloseSignal { get; private set; }

        public override void Execute()
        {
            Time.timeScale = NewPauseState ? 0f : 1f;
            if(NewPauseState)
            {
                PausePopupOpenSignal.Dispatch();
            }
            else
            {
                PausePopupCloseSignal.Dispatch();
            }
        }

    }

}