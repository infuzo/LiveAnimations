
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

public enum EventCommandType
{
    AppStart,
    ChangeCoinsBalance,
    CoinsBalanceChanged
}

public class MainContext : EventCommandContext
{
    
    public MainContext(MonoBehaviour view) : base(view)
    {

    }

    protected override void mapBindings()
    {
        base.mapBindings();

        injectionBinder.Bind<ICoroutineExecuter>().To<CoroutineExecuter>().ToSingleton();
        injectionBinder.Bind<UserDataModel>().ToSingleton();
        injectionBinder.Bind<ISocial>().To<FacebookSocial>().ToName(SocialNetworkType.Facebook);
        injectionBinder.Bind<ISocial>().To<TwitterSocial>().ToName(SocialNetworkType.Twitter);

        commandBinder.Bind(EventCommandType.AppStart).InSequence().To<ShowLoadingCommand>().To<AppStartCommand>().To<HideLoadingCommand>().Once();
        commandBinder.Bind(EventCommandType.ChangeCoinsBalance).To<ChangeCoinsCommand>();
        commandBinder.Bind(EventCommandType.CoinsBalanceChanged).To<CoinsBalanceChangedCommand>();

    }

}
