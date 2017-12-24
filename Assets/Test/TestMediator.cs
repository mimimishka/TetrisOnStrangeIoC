using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

namespace Test
{
    public class TestMediator : Mediator
    {
        [Inject]
        public TestView view { get; set; }

        [Inject]
        public ClickSignal signal { get; set; }

        public override void OnRegister()
        {
            view.Dispatcher.AddListener(TestView.CLICK_EVENT, OnButtonEvent);
            view.init();
        }
        void OnButtonEvent()
        {
            Debug.Log("on click event");
            signal.Dispatch();
        }
    }
}