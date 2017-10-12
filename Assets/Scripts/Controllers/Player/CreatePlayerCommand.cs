using strange.extensions.command.impl;
using System.Collections;
using UnityEngine;
using Runner.Views;
using Runner.Models;

namespace Runner.Controllers.Player
{
    public class CreatePlayerCommand : Command
    {

        const byte startLineIndex = 1;

        [Inject]
        public PlayerModel PlayerModel { get; private set; }
        [Inject]
        public PlayerView PlayerView { get; private set; }

        [Inject]
        public MovementSignal MovementSignal { get; private set; }
        [Inject]
        public CheckForNewWaypointSignal CheckForNewWaypointSignal { get; private set; }
        [Inject]
        public DebugLineSignal DebugLineSignal { get; private set; }
        [Inject]
        public CollisionSignal CollisionSignal { get; private set; }

        [Inject]
        public ChangeRespawnInvulnerabitiySignal ChangeRespawnInvulnerabitiySignal { get; private set; }

        public override void Execute()
        {
            PlayerModel.CurrentLineIndex = startLineIndex;
            PlayerModel.TargetWayPoint = GameWorldModel.Instance.AllWaypoints[startLineIndex].First.Next.Value;

            PlayerView.InitSignalInstances(MovementSignal, CheckForNewWaypointSignal, DebugLineSignal, CollisionSignal);
            PlayerView.Transform.position = GameWorldModel.Instance.AllWaypoints[startLineIndex].First.Value.position;
            PlayerView.Animator.SetFloat("Speed", 1f);

            ChangeRespawnInvulnerabitiySignal.Dispatch();
        }

    }
}