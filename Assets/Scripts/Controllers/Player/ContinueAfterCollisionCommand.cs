using strange.extensions.signal.impl;
using strange.extensions.command.impl;
using UnityEngine;

namespace Runner.Controllers.Player
{

    public class ContinueAfterCollisionSignal : Signal { }

    public class ContinueAfterCollisionCommand : Command
    {

        [Inject]
        public ChangeLifesCountSignal ChangeLifesCountSignal { get; private set; }
        [Inject]
        public ChangeRespawnInvulnerabitiySignal ChangeRespawnInvulnerabitiySignal { get; private set; }
        [Inject]
        public UI.LoosePopupCloseSignal LoosePopupCloseSignal { get; private set; }

        public override void Execute()
        {
            ChangeLifesCountSignal.Dispatch(-1);
            Time.timeScale = 1f;
            ChangeRespawnInvulnerabitiySignal.Dispatch();
            LoosePopupCloseSignal.Dispatch();
        }

    }
}