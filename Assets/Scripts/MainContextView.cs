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

    }
}