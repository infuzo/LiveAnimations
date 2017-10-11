using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using System.Collections;
using UnityEngine;

namespace Runner.Controllers
{
    public class AppStartSignal : Signal { }

    public class AppStartCommand : Command
    {

        public override void Execute()
        {
            Debug.Log("Game started.");
        }

    }
}