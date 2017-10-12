using strange.extensions.command.impl;
using System.Collections;
using UnityEngine;
using Runner.Views;

namespace Runner.Controllers.Player
{
    public class CreatePlayerCommand : Command
    {

        const byte startLineIndex = 1;

        [Inject]
        public PlayerView PlayerView { get; private set; }
        [Inject]
        public MovementSignal MovementSignal { get; private set; }
        [Inject]
        public CheckForNewWaypointSignal CheckForNewWaypointSignal { get; private set; }
        [Inject]
        public DebugLineSignal DebugLineSignal { get; private set; }

        public override void Execute()
        {
            PlayerView.InitSignalInstances(MovementSignal, CheckForNewWaypointSignal, DebugLineSignal);

            PlayerView.CurrentLineIndex = startLineIndex;
            PlayerView.Transform.position = GameWorldModel.Instance.AllWaypoints[startLineIndex].First.Value.position;
            PlayerView.TargetWayPoint = GameWorldModel.Instance.AllWaypoints[startLineIndex].First.Next.Value;
            PlayerView.Animator.SetFloat("Speed", 1f);
        }

    }
}