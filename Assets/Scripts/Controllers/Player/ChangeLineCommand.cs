using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using Runner.Models;

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
        public PlayerModel PlayerModel { get; private set; }

        public override void Execute()
        {
            
            switch(ChangeLineType)
            {
                case ChangeLineType.Left:
                    if(PlayerModel.CurrentLineIndex > 0)
                    {
                        ChangeLine(PlayerModel.CurrentLineIndex - 1);
                    }
                    break;
                case ChangeLineType.Right:
                    if (PlayerModel.CurrentLineIndex < GameWorldModel.LinesCount - 1)
                    {
                        ChangeLine(PlayerModel.CurrentLineIndex + 1);
                    }
                    break;
            }

        }

        void ChangeLine(int newLineIndex)
        {
            int currentWayPointIndex = GameWorldModel.Instance.FindIndexOfWaypoint(PlayerModel.CurrentLineIndex, PlayerModel.TargetWayPoint);
            PlayerModel.CurrentLineIndex = (byte)newLineIndex;
            PlayerModel.TargetWayPoint = GameWorldModel.Instance.GetWaypointByIndex(PlayerModel.CurrentLineIndex, currentWayPointIndex);
        }

    }

}