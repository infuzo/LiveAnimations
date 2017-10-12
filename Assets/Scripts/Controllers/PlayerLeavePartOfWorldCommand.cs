using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using Runner.Views;
using Runner.Services;
using System.Collections;

namespace Runner.Controllers
{
    public class PlayerLeavePartOfWorldSignal : Signal <IPartOfWorldView> { }

    public class PlayerLeavePartOfWorldCommand : Command
    {

        const float distanceFromEndPointToPlayerForDestroy = 10f;

        [Inject]
        public IPartOfWorldView LeavedPart { get; private set; }

        [Inject]
        public IPartsOfWorldManager PartsOfWorldManager { get; private set; }
        [Inject]
        public PlayerView PlayerView { get; private set; }
        [Inject]
        public ICoroutineExecuter CoroutineExecuter { get; private set; }
        [Inject]
        public IObstaclesManager ObstaclesManager { get; private set; }

        public override void Execute()
        {
            Retain();
            CoroutineExecuter.Execute(CoroutineWaitForRemove());
        }

        IEnumerator CoroutineWaitForRemove()
        {
            while(Vector3.Distance(PlayerView.Transform.position, LeavedPart.EndPointPosition) < distanceFromEndPointToPlayerForDestroy)
            {
                yield return new WaitForFixedUpdate();
            }

            ObstaclesManager.RemoveAllObstaclesFromPartOfWorld(GameWorldModel.Instance.PartsOfWorld.Peek());
            PartsOfWorldManager.RemoveOldestPartOfWorld();
            Release();
        }

    }

}