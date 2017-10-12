using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using Runner.Views;
using Runner.Services;

namespace Runner.Controllers
{
    public class PlayerLeavePartOfWorldSignal : Signal { }

    public class PlayerLeavePartOfWorldCommand : Command
    {
        [Inject]
        public IPartsOfWorldManager PartsOfWorldManager { get; private set; }

        public override void Execute()
        {
            PartsOfWorldManager.RemoveOldestPartOfWorld();
        }

    }

}