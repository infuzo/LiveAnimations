using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Controllers.Player;
using strange.extensions.command.impl;

namespace Runner.Views
{
    public class PlayerView 
    {


        class PlayerViewWorker : MonoBehaviour
        {

            public Transform CachedTransform;
            public Animator CachedAnimator;
            public Rigidbody CachedRigidbody;

            #region Signal instances

            public MovementSignal MovementSignal { get; set; }

            #endregion

            private void FixedUpdate()
            {
                MovementSignal.Dispatch();
            }

        }

        public byte CurrentLineIndex { get; set; }
        public float CurrentSideAnimationVelocity { get; set; }
        public Transform PreviousWayPoint { get; private set; }

        PlayerViewWorker currentPlayer;
        Transform targetWayPoint;

        


        public PlayerView()
        {
            currentPlayer = MonoBehaviour.Instantiate(GameWorldModel.Instance.PlayerPrefab).AddComponent<PlayerViewWorker>();
        }

        public void InitSignalInstances(MovementSignal movementSignal)
        {
            currentPlayer.MovementSignal = movementSignal;
        }

        public Animator Animator
        {
            get
            {
                if (currentPlayer.CachedAnimator == null) { currentPlayer.CachedAnimator = currentPlayer.GetComponent<Animator>(); }
                return currentPlayer.CachedAnimator;
            }
        }

        public Rigidbody Rigidbody
        {
            get
            {
                if (currentPlayer.CachedRigidbody == null) { currentPlayer.CachedRigidbody = currentPlayer.GetComponent<Rigidbody>(); }
                return currentPlayer.CachedRigidbody;
            }
        }

        public Transform Transform
        {
            get
            {
                if (currentPlayer.CachedTransform == null) { currentPlayer.CachedTransform = currentPlayer.transform; }
                return currentPlayer.CachedTransform;
            }
        }

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


