using strange.extensions.command.impl;
using UnityEngine;

public class ChangeCoinsCommand : Command
{

    [Inject]
    public UserDataModel UserDataModel { get; private set; }
    [Inject]
    public int AmountOfChange { get; private set; }

    [Inject]
    public CoinsBalanceChangedSignal CoinsBalanceChangedSignal { get; private set; }

    public override void Execute()
    {
        if(UserDataModel.CurrencyBalance + AmountOfChange < 0)
        {
            return;
        }

        UserDataModel.CurrencyBalance += AmountOfChange;
        CoinsBalanceChangedSignal.Dispatch();
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
