using UnityEngine;
using strange.extensions.command.impl;

namespace Runner.Controllers
{

    public class CreateInputManagerByPlatform : Command
    {

        [Inject]
        public Player.ChangeLineSignal ChangeLineSignal { get; private set; }

        public override void Execute()
        {
            Services.IInputManager inputManager = null;

            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                case RuntimePlatform.IPhonePlayer:
                    inputManager = injectionBinder.GetInstance<Services.IInputManager>(Services.InputManager.InputManagerType.Touch);
                    break;
                default:
                    inputManager = injectionBinder.GetInstance<Services.IInputManager>(Services.InputManager.InputManagerType.Keyborad);
                    break;
            }

            inputManager.OnInputAction.AddListener(input =>
            {
                switch (input)
                {
                    case Services.InputManager.InputType.LeftCommand:
                        ChangeLineSignal.Dispatch(Player.ChangeLineType.Left);
                        break;
                    case Services.InputManager.InputType.RightCommand:
                        ChangeLineSignal.Dispatch(Player.ChangeLineType.Right);
                        break;
                }
            });
        }

    }

}