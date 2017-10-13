using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Runner.Controllers.UI
{

    public class GameScreenChangeLifesCountSignal : Signal { }

    public class GameScreenChangeLifesCountCommand : Command
    {

        [Inject]
        public Views.UI.GameScreenView GameScreenView { get; private set; }
        [Inject]
        public Models.PlayerModel PlayerModel { get; private set; }

        public override void Execute()
        {
            GameScreenView.SetLifesCount(PlayerModel.CurrentLifesCount);
        }

    }

}
