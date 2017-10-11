using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;
using Runner.Controllers;
using Runner.Services;

namespace Runner
{
    public class MainContext : SignalContext
    {

        public MainContext(MonoBehaviour view) : base(view)
        {

        }

        protected override void mapBindings()
        {
            base.mapBindings();

            injectionBinder.Bind<ICoroutineExecuter>().To<CoroutineExecuter>().ToSingleton();
            injectionBinder.Bind<IPartsOfWorldManager>().To<PartsOfWorldManager>().ToSingleton();

            commandBinder.Bind<AppStartSignal>().InSequence().To<AppStartCommand>().To<ChangedPartsOfWorldAmountCommand>().Once();
            commandBinder.Bind<ChangedPartsOfWorldAmountSignal>().To<ChangedPartsOfWorldAmountCommand>();
            commandBinder.Bind<PlayerLeavePartOfWorldSignal>().InSequence().To<PlayerLeavePartOfWorldCommand>();
        }

    }
}