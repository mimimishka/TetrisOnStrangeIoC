using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace Tetris
{
    public class RestartView : View
    {
        [Inject]
        public IEventDispatcher Dispatcher { get; private set; }
        internal const string RESTART_EVENT = "restart";
        void OnClick()
        {
            Dispatcher.Dispatch(RESTART_EVENT);
        }
        internal void Init()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }
    }
}