
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

public class MainContext : SignalContext
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

        commandBinder.Bind<AppStartSignal>().InSequence().To<ShowLoadingCommand>().To<AppStartCommand>().To<HideLoadingCommand>().Once();
        commandBinder.Bind<ChangeCoinsSignal>().To<ChangeCoinsCommand>();
        commandBinder.Bind<CoinsBalanceChangedSignal>().To<CoinsBalanceChangedCommand>();

    }

}
