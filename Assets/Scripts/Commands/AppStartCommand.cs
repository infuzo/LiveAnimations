using strange.extensions.command.impl;
using System.Collections;
using UnityEngine;

public class AppStartCommand : EventCommand
{

    [Inject]
    public ICoroutineExecuter CoroutineExecuter { get; private set; }

    [Inject(SocialNetworkType.Facebook)]
    public ISocial FacebookSocial { get; private set; }
    [Inject(SocialNetworkType.Twitter)]
    public ISocial TwitterSocial { get; private set; }

    public override void Execute()
    {
        Retain();

        Debug.Log("AppStartCommand executed.");
        CoroutineExecuter.Execute(CoroutineLongAction());
    }

    IEnumerator CoroutineLongAction()
    {
        yield return new WaitForSeconds(2f);

        dispatcher.Dispatch(EventCommandType.ChangeCoinsBalance, 100);
        dispatcher.Dispatch(EventCommandType.ChangeCoinsBalance, 50);
        
        FacebookSocial.Login();
        TwitterSocial.Login();

        Release();
    }

}
