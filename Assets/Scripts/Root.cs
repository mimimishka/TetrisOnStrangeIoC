using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.impl;
using UnityEngine.UI;

namespace Tetris
{
    public class Root : ContextView
    {
        void Awake()
        {
            var context = new MainContext(this);
        }
    }
}