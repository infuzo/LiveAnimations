using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runner.Controllers.UI
{

    public enum LoosePopupButtonType
    {
        Continue,
        Replay
    }

    /// <summary>
    /// Arguments: available continue.
    /// </summary>
    public class LoosePopupOpenSignal : Signal<bool> { }

    public class LoosePopupCloseSignal : Signal { }

    public class LoosePopupButtonClickSignal : Signal<LoosePopupButtonType> { }

    public class LoosePopupOpenCommand : Command
    {

        [Inject]
        public bool AvailableContinue { get; private set; }

        [Inject]
        public Services.UI.IGameUIManager GameUIManager { get; private set; }
        [Inject]
        public Models.PlayerModel PlayerModel { get; private set; }

        public override void Execute()
        {
            Views.UI.LoosePopupView newPopup = GameUIManager.OpenPopup<Views.UI.LoosePopupView>();
            newPopup.SetAttempsRemaningCount(PlayerModel.CurrentLifesCount);
            newPopup.SetButtonContinueVisible(AvailableContinue);
        }

    }

    public class LoosePopupCloseCommand : Command
    {
        [Inject]
        public Services.UI.IGameUIManager GameUIManager { get; private set; }

        public override void Execute()
        {
            GameUIManager.ClosePopup<Views.UI.LoosePopupView>();
        }
    }

    public class LoosePopupButtonClickCommand : Command
    {
        [Inject]
        public LoosePopupButtonType ClickedButtonType { get; private set; }
        
        [Inject]
        public Player.ContinueAfterCollisionSignal ContinueAfterCollisionSignal { get; private set; }

        public override void Execute()
        {
            switch(ClickedButtonType)
            {
                case LoosePopupButtonType.Continue:
                    ContinueAfterCollisionSignal.Dispatch();
                    break;
                case LoosePopupButtonType.Replay:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;
            }
        }
    }

}