using strange.extensions.context.impl;
using UnityEngine;
using System.Collections.Generic;

namespace Runner
{
    public class MainContextView : ContextView
    {
        
        void Start()
        {
            var context = new MainContext(this);
            context.Start();
        }

        [Inject]
        public Controllers.Player.ChangeLineSignal ChangeLineSignal { get; private set; } //TODO: remove

        private void Update() //TODO: remove
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeLineSignal.Dispatch(Controllers.Player.ChangeLineType.Left);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeLineSignal.Dispatch(Controllers.Player.ChangeLineType.Right);
            }
        }

    }
}