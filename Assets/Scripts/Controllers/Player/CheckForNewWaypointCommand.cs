using UnityEngine;
using Runner.Models;
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
        public PlayerModel PlayerModel { get; private set; }
        [Inject]
        public PlayerView PlayerView { get; private set; }

        public override void Execute()
        {
            Vector2 positionWithoutYPlayer = new Vector2(PlayerView.Transform.position.x, PlayerView.Transform.position.z);
            Vector2 positionWithoutYWaypoint = new Vector2(PlayerModel.TargetWayPoint.position.x, PlayerModel.TargetWayPoint.position.z);

            if(Vector2.Distance(positionWithoutYPlayer, positionWithoutYWaypoint) <= distanceToChangeWaypoint)
            {
                PlayerModel.TargetWayPoint = GameWorldModel.Instance.AllWaypoints[PlayerModel.CurrentLineIndex].Find(PlayerModel.TargetWayPoint).Next.Value;
            }
        }

    }
}