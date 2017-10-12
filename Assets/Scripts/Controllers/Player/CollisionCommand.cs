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

        public override void Execute()
        {
            if(Collision.collider.CompareTag(GameWorldModel.ObstacleTag))
            {
                Debug.Log("Obstacle collision");
            }
        }

    }
}