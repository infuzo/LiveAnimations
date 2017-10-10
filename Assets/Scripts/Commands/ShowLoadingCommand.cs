using strange.extensions.command.impl;
using UnityEngine;

public class ShowLoadingCommand : Command
{

    public override void Execute()
    {
        Debug.Log("Loading shown.");    
    }

}
