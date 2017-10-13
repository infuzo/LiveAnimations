using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Models
{
    public class PlayerModel
    {

        public const string LayerNameDefault = "Default";
        public const string LayerNameInvulnerable = "Invulnerable";

        public const float RespawnInvulnerabilityDuration = 3f;

        public byte CurrentLineIndex { get; set; }

        public float CurrentSideAnimationVelocity { get; set; }

        public Transform PreviousWayPoint { get; private set; }

        public byte CurrentLifesCount { get; set; }

        Transform targetWayPoint;

        public Transform TargetWayPoint
        {
            get
            {
                return targetWayPoint;
            }
            set
            {
                targetWayPoint = value;
                PreviousWayPoint = GameWorldModel.Instance.AllWaypoints[CurrentLineIndex].Find(targetWayPoint).Previous.Value;
            }
        }

    }

}