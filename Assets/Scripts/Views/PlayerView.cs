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

            private void FixedUpdate()
            {
                MainContext.InjectionBinder.GetInstance<MovementSignal>().Dispatch();
                MainContext.InjectionBinder.GetInstance<CheckForNewWaypointSignal>().Dispatch();
            }

            private void OnDrawGizmos()
            {
                MainContext.InjectionBinder.GetInstance<DebugLineSignal>().Dispatch();
            }

            private void OnCollisionEnter(Collision collision)
            {
                MainContext.InjectionBinder.GetInstance<CollisionSignal>().Dispatch(collision);
            }

        }

        PlayerViewWorker currentPlayer;

        public PlayerView()
        {
            currentPlayer = MonoBehaviour.Instantiate(GameWorldModel.Instance.PlayerPrefab).AddComponent<PlayerViewWorker>();
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




