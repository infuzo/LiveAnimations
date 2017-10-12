using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Controllers.Player;

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
            public CheckForNewWaypointSignal CheckForNewWaypointSignal { get; set; }
            public DebugLineSignal DebugLineSignal { get; set; }
            public CollisionSignal CollisionSignal { get; set; }

            #endregion

            private void FixedUpdate()
            {
                MovementSignal.Dispatch();
                CheckForNewWaypointSignal.Dispatch();
            }

            private void OnDrawGizmos()
            {
                DebugLineSignal.Dispatch();
            }

            private void OnCollisionEnter(Collision collision)
            {
                CollisionSignal.Dispatch(collision);
            }

        }

        PlayerViewWorker currentPlayer;

        public PlayerView()
        {
            currentPlayer = MonoBehaviour.Instantiate(GameWorldModel.Instance.PlayerPrefab).AddComponent<PlayerViewWorker>();
        }

        public void InitSignalInstances(MovementSignal movementSignal, CheckForNewWaypointSignal checkForNewWaypointSignal, DebugLineSignal debugLineSignal, CollisionSignal collisionSignal)
        {
            currentPlayer.MovementSignal = movementSignal;
            currentPlayer.CheckForNewWaypointSignal = checkForNewWaypointSignal;
            currentPlayer.DebugLineSignal = debugLineSignal;
            currentPlayer.CollisionSignal = collisionSignal;
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
        
        public void SetLayer(string layerName)
        {
            currentPlayer.gameObject.layer = LayerMask.NameToLayer(layerName);
        }

        public Material MainMaterial
        {
            get
            {
                return currentPlayer.GetComponentInChildren<SkinnedMeshRenderer>().material;
            }
        }

    }
}




