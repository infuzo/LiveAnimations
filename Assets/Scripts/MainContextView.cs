using strange.extensions.context.impl;
using UnityEngine;

namespace Runner
{
    public class MainContextView : ContextView
    {

        [Inject]
        public Controllers.PlayerLeavePartOfWorldSignal PlayerLeavePartOfWorldSignal { get; private set; }

        void Start()
        {
            var context = new MainContext(this);
            context.Start();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerLeavePartOfWorldSignal.Dispatch();
            }
        }

    }
}