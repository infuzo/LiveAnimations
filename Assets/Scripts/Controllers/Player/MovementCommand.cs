﻿using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using Runner.Views;

namespace Runner.Controllers.Player
{

    public class MovementSignal : Signal { }

    public class MovementCommand : Command
    {

        [Inject]
        public PlayerView PlayerView { get; private set; }

        const float maxSideDistance = .75f;
        const float minSideDistance = .475f;
        const float sideAnimationChangeVelocity = 5f;

        public override void Execute()
        {
            Vector3 directionFromCurrentToPreviousWaypoint = PlayerView.PreviousWayPoint.position - PlayerView.TargetWayPoint.position;
            Vector3 playerDirectionToTargetWaypoint = PlayerView.TargetWayPoint.position - PlayerView.Transform.position;

            float sideDistance = Vector3.Distance(PlayerView.TargetWayPoint.position, PlayerView.Transform.position) *
                Mathf.Sin((180f - Vector3.Angle(directionFromCurrentToPreviousWaypoint, playerDirectionToTargetWaypoint)) * Mathf.Deg2Rad);
            //Factor for side movement (-1...1) if player not run on current line
            float sideFactor = (Mathf.Clamp(sideDistance, minSideDistance, maxSideDistance) - minSideDistance) / (maxSideDistance - minSideDistance) *
                Mathf.Sign(Vector3.Dot(PlayerView.Transform.right, playerDirectionToTargetWaypoint.normalized));
            Vector3 movementDirection = PlayerView.Transform.forward * GameWorldModel.Instance.ForwardSpeed + PlayerView.Transform.right * sideFactor * GameWorldModel.Instance.SideSpeed;

            PlayerView.Rigidbody.MovePosition(PlayerView.Transform.position + (movementDirection * Time.fixedDeltaTime));

            //Smoothing side animation changing
            PlayerView.CurrentSideAnimationVelocity = Mathf.MoveTowards(PlayerView.CurrentSideAnimationVelocity, sideFactor, Time.fixedDeltaTime * sideAnimationChangeVelocity);

            PlayerView.Animator.SetFloat("Angle", PlayerView.CurrentSideAnimationVelocity * 2f);

        }

    }
}