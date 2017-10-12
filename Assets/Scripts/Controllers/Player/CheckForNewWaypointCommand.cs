using UnityEngine;
using Runner.Views;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Runner.Controllers.Player
{
    public class CheckForNewWaypointSignal : Signal { }

    public class CheckForNewWaypointCommand : Command
    {

        const float distanceToChangeWaypoint = 3f;

        [Inject]
        public PlayerView PlayerView { get; private set; }

        public override void Execute()
        {
            Vector2 positionWithoutYPlayer = new Vector2(PlayerView.Transform.position.x, PlayerView.Transform.position.z);
            Vector2 positionWithoutYWaypoint = new Vector2(PlayerView.TargetWayPoint.position.x, PlayerView.TargetWayPoint.position.z);

            if(Vector2.Distance(positionWithoutYPlayer, positionWithoutYWaypoint) <= distanceToChangeWaypoint)
            {
                PlayerView.TargetWayPoint = GameWorldModel.Instance.AllWaypoints[PlayerView.CurrentLineIndex].Find(PlayerView.TargetWayPoint).Next.Value;
            }
        }

    }
}