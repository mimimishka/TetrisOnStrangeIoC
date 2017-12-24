using System.Collections;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine.UI;

namespace Test 
{ 
    public class TestView : View
    {
        internal const string CLICK_EVENT = "click ev";
        [Inject]
        public IEventDispatcher Dispatcher { get; private set; }

        internal void init()
        {
            Debug.Log("init");
            GetComponent<Button>().onClick.AddListener(OnClick);
        }
        void OnClick()
        {
            Debug.Log("click");
            Dispatcher.Dispatch(CLICK_EVENT);
        }
    }
}