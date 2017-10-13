using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Runner.Controllers.UI
{

    public class PausePopupOpenSignal : Signal { }

    public class PausePopupCloseSignal : Signal { }

    public class PausePopupOpenCommand : Command
    {
        [Inject]
        public Services.UI.IGameUIManager GameUIManager { get; private set; }

        public override void Execute()
        {
            GameUIManager.OpenPopup<Views.UI.PausePopupView>();
        }
    }

    public class PausePopupCloseCommand : Command
    {
        [Inject]
        public Services.UI.IGameUIManager GameUIManager { get; private set; }

        public override void Execute()
        {
            GameUIManager.ClosePopup<Views.UI.PausePopupView>();
        }
    }

}

