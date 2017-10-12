using UnityEngine;
using Runner.Controllers;
using Runner.Controllers.Player;
using Runner.Services;
using Runner.Views;

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
            injectionBinder.Bind<PlayerView>().To<PlayerView>().ToSingleton();

            commandBinder.Bind<AppStartSignal>().InSequence().To<AppStartCommand>().To<ChangedPartsOfWorldAmountCommand>().To<CreatePlayerCommand>().Once();
            commandBinder.Bind<ChangedPartsOfWorldAmountSignal>().To<ChangedPartsOfWorldAmountCommand>();
            commandBinder.Bind<PlayerLeavePartOfWorldSignal>().InSequence().To<PlayerLeavePartOfWorldCommand>();
            commandBinder.Bind<ChangeLineSignal>().To<ChangeLineCommand>().Pooled();
            commandBinder.Bind<MovementSignal>().To<MovementCommand>().Pooled();
            commandBinder.Bind<CheckForNewWaypointSignal>().To<CheckForNewWaypointCommand>().Pooled();
            commandBinder.Bind<DebugLineSignal>().To<DebugLineCommand>().Pooled();

        }

    }
}