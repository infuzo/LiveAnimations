using strange.extensions.command.impl;
using System.Collections;
using UnityEngine;

public class AppStartCommand : Command
{

    [Inject]
    public ICoroutineExecuter CoroutineExecuter { get; private set; }
    [Inject]
    public ChangeCoinsSignal ChangeCoinsSignal { get; private set; }
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

        ChangeCoinsSignal.Dispatch(50);
        FacebookSocial.Login();
        TwitterSocial.Login();

        Release();
    }

}
