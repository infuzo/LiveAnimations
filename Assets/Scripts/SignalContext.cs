using UnityEngine;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;

public class SignalContext : MVCSContext
{

    public SignalContext(MonoBehaviour contextView) : base(contextView, ContextStartupFlags.MANUAL_MAPPING)
    {

    }

    protected override void addCoreComponents()
    {
        base.addCoreComponents();

        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<EventCommandBinder>().ToSingleton();
    }

    public override void Launch()
    {
        base.Launch();

        dispatcher.Dispatch(EventCommandType.AppStart);
    }

}
