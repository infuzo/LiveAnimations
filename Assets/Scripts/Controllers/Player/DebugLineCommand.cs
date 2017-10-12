using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Runner.Controllers.Player
{
    public class DebugLineSignal : Signal { }

    public class DebugLineCommand : Command
    {

        [Inject]
        public Views.PlayerView PlayerView { get; private set; }

        public override void Execute()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(PlayerView.Transform.position, PlayerView.TargetWayPoint.position);
        }

    }
}