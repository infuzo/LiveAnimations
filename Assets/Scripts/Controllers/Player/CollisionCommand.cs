using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Runner.Controllers.Player
{

    public  class CollisionSignal : Signal<Collision> { }

    public class CollisionCommand : Command
    {

        [Inject]
        public Collision Collision { get; private set; }
        [Inject]
        public UI.LoosePopupOpenSignal LoosePopupOpenSignal { get; private set; }
        [Inject]
        public Models.PlayerModel PlayerModel { get; private set; }

        public override void Execute()
        {
            if(Collision.collider.CompareTag(GameWorldModel.ObstacleTag))
            {
                Time.timeScale = 0f;
                LoosePopupOpenSignal.Dispatch(PlayerModel.CurrentLifesCount > 0);
            }
        }

    }
}