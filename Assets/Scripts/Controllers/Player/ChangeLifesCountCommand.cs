using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Runner.Controllers.Player
{

    /// <summary>
    /// Arguments: amount of change (-1 for decrement).
    /// </summary>
    public class ChangeLifesCountSignal : Signal<short> { }

    public class ChangeLifesCountCommand : Command
    {

        [Inject]
        public short ChangeAmount { get; private set; }
        [Inject]
        public UI.GameScreenChangeLifesCountSignal GameScreenChangeLifesCountSignal { get; private set; }
        [Inject]
        public Models.PlayerModel PlayerModel { get; private set; }

        public override void Execute()
        {
            if(PlayerModel.CurrentLifesCount <= 0) { return; }

            PlayerModel.CurrentLifesCount = (byte)(PlayerModel.CurrentLifesCount + ChangeAmount);
            GameScreenChangeLifesCountSignal.Dispatch();
        }

    }

}