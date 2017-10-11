using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using Runner.Services;

namespace Runner.Controllers
{

    public class ChangedPartsOfWorldAmountSignal : Signal { }

    public class ChangedPartsOfWorldAmountCommand : Command
    {

        [Inject]
        public ICoroutineExecuter CoroutineExecuter { get; private set; }
        [Inject]
        public IPartsOfWorldManager PartsOfWorldManager { get; private set; }

        public override void Execute()
        {
            Retain();
            CoroutineExecuter.Execute(CoroutineCreatePartsWhileNeed());
        }

        IEnumerator CoroutineCreatePartsWhileNeed()
        {
            while(PartsOfWorldManager.CreateNextPartOfWorld())
            {
                yield return new WaitForEndOfFrame();
            }
            Release();
        }

    }
}