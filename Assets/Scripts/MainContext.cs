using UnityEngine;
using Runner.Controllers;
using Runner.Controllers.Player;
using Runner.Services;
using Runner.Views;
using Runner.Views.UI;
using Runner.Models;

namespace Runner
{
    public class MainContext : SignalContext
    {

        public static strange.extensions.injector.api.ICrossContextInjectionBinder InjectionBinder { get; private set; }

        public MainContext(MonoBehaviour view) : base(view)
        {
            Time.timeScale = 1f;
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            injectionBinder.Bind<ICoroutineExecuter>().To<CoroutineExecuter>().ToSingleton();
            injectionBinder.Bind<IPartsOfWorldManager>().To<PartsOfWorldManager>().ToSingleton();
            injectionBinder.Bind<IObstaclesManager>().To<ObstaclesManager>().ToSingleton();
            injectionBinder.Bind<IGameManager>().To<GameManager>().ToSingleton();
            injectionBinder.Bind<Services.UI.IGameUIManager>().To<Services.UI.GameUIManager>().ToSingleton();
            injectionBinder.Bind<PlayerModel>().To<PlayerModel>().ToSingleton();
            injectionBinder.Bind<PlayerView>().To<PlayerView>().ToSingleton();
            injectionBinder.Bind<IInputManager>().To<TouchInputManager>().ToName(InputManager.InputManagerType.Touch).ToSingleton();
            injectionBinder.Bind<IInputManager>().To<KeyBoradInputManager>().ToName(InputManager.InputManagerType.Keyborad).ToSingleton();

            injectionBinder.Bind<GameScreenView>().To<GameScreenView>().ToSingleton();
            injectionBinder.Bind<PausePopupView>().To<PausePopupView>();
            injectionBinder.Bind<LoosePopupView>().To<LoosePopupView>();

            commandBinder.Bind<AppStartSignal>().InSequence().To<AppStartCommand>().To<ChangedPartsOfWorldAmountCommand>().To<CreatePlayerCommand>()
                .To<CreateInputManagerByPlatform>().To<Controllers.UI.CreateGameUICommand>().Once();
            commandBinder.Bind<ChangedPartsOfWorldAmountSignal>().To<ChangedPartsOfWorldAmountCommand>();
            commandBinder.Bind<PlayerLeavePartOfWorldSignal>().To<PlayerLeavePartOfWorldCommand>();
            commandBinder.Bind<ChangeElapsedTimeSignal>().To<ChangeElapsedTimeCommand>().Pooled();
            commandBinder.Bind<ChangePauseStateSignal>().To<ChangePauseStateCommand>();

            commandBinder.Bind<ChangeLineSignal>().To<ChangeLineCommand>().Pooled();
            commandBinder.Bind<MovementSignal>().To<MovementCommand>().Pooled();
            commandBinder.Bind<CheckForNewWaypointSignal>().To<CheckForNewWaypointCommand>().Pooled();
            commandBinder.Bind<DebugLineSignal>().To<DebugLineCommand>().Pooled();
            commandBinder.Bind<CollisionSignal>().To<CollisionCommand>();
            commandBinder.Bind<ChangeInvulnerabilitySignal>().To<ChangeInvulnerabilityCommand>();
            commandBinder.Bind<ChangeRespawnInvulnerabitiySignal>().To<ChangeRespawnInvulnerabitiyCommand>();
            commandBinder.Bind<ChangeLifesCountSignal>().To<ChangeLifesCountCommand>();
            commandBinder.Bind<ContinueAfterCollisionSignal>().To<ContinueAfterCollisionCommand>();

            commandBinder.Bind<Controllers.UI.GameScreenChangeElapsedTimeSignal>().To<Controllers.UI.GameScreenChangeElapsedTimeCommand>().Pooled();
            commandBinder.Bind<Controllers.UI.GameScreenChangeLifesCountSignal>().To<Controllers.UI.GameScreenChangeLifesCountCommand>();
            commandBinder.Bind<Controllers.UI.PausePopupOpenSignal>().To<Controllers.UI.PausePopupOpenCommand>();
            commandBinder.Bind<Controllers.UI.PausePopupCloseSignal>().To<Controllers.UI.PausePopupCloseCommand>();
            commandBinder.Bind<Controllers.UI.LoosePopupOpenSignal>().To<Controllers.UI.LoosePopupOpenCommand>();
            commandBinder.Bind<Controllers.UI.LoosePopupCloseSignal>().To<Controllers.UI.LoosePopupCloseCommand>();
            commandBinder.Bind<Controllers.UI.LoosePopupButtonClickSignal>().To<Controllers.UI.LoosePopupButtonClickCommand>();


            InjectionBinder = injectionBinder;
        }

    }
}