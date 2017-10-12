using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using Runner.Views;

namespace Runner.Controllers.Player
{

    public enum ChangeLineType
    {
        Left,
        Right
    }

    public class ChangeLineSignal : Signal<ChangeLineType> { }

    public class ChangeLineCommand : Command
    {

        [Inject]
        public ChangeLineType ChangeLineType { get; private set; }
        [Inject]
        public PlayerView PlayerView { get; private set; }

        public override void Execute()
        {
            
            switch(ChangeLineType)
            {
                case ChangeLineType.Left:
                    if(PlayerView.CurrentLineIndex > 0)
                    {
                        ChangeLine(PlayerView.CurrentLineIndex - 1);
                    }
                    break;
                case ChangeLineType.Right:
                    if (PlayerView.CurrentLineIndex < GameWorldModel.LinesCount - 1)
                    {
                        ChangeLine(PlayerView.CurrentLineIndex + 1);
                    }
                    break;
            }

        }

        void ChangeLine(int newLineIndex)
        {
            int currentWayPointIndex = GameWorldModel.Instance.FindIndexOfWaypoint(PlayerView.CurrentLineIndex, PlayerView.TargetWayPoint);
            PlayerView.CurrentLineIndex = (byte)newLineIndex;
            PlayerView.TargetWayPoint = GameWorldModel.Instance.GetWaypointByIndex(PlayerView.CurrentLineIndex, currentWayPointIndex);
        }

    }

}