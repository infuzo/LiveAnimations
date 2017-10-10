using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;
using UnityEngine;

public class ChangeCoinsCommand : EventCommand
{

    [Inject]
    public UserDataModel UserDataModel { get; private set; }

    public override void Execute()
    {
        if (UserDataModel.CurrencyBalance + (int)((TmEvent)data).data < 0)
        {
            return;
        }

        UserDataModel.CurrencyBalance += (int)((TmEvent)data).data;
        dispatcher.Dispatch(EventCommandType.CoinsBalanceChanged);
    }

}


public class CoinsBalanceChangedCommand : Command
{

    [Inject]
    public UserDataModel UserDataModel { get; private set; }

    public override void Execute()
    {
        Debug.Log(string.Format("New coins balance: {0}.", UserDataModel.CurrencyBalance));
    }

}
