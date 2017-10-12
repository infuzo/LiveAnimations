using strange.extensions.signal.impl;
using strange.extensions.command.impl;
using Runner.Models;
using System.Collections;
using UnityEngine;

namespace Runner.Controllers.Player
{
    public class ChangeRespawnInvulnerabitiySignal : Signal { }


    public class ChangeRespawnInvulnerabitiyCommand : Command
    {

        [Inject]
        public Services.ICoroutineExecuter CoroutineExecuter { get; private set; }
        [Inject]
        public ChangeInvulnerabilitySignal ChangeInvulnerabilitySignal { get; private set; }

        public override void Execute()
        {
            Retain();
            CoroutineExecuter.Execute(CoroutineInvulnerabitiy());
        }

        IEnumerator CoroutineInvulnerabitiy()
        {
            ChangeInvulnerabilitySignal.Dispatch(true);
            yield return new WaitForSeconds(PlayerModel.RespawnInvulnerabilityDuration);
            ChangeInvulnerabilitySignal.Dispatch(false);
            Release();
        }

    }
}