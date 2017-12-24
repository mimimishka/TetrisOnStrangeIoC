using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace Tetris
{
    public class RestartMediator : Mediator
    {
        [Inject]
        public RestartSignal RestartSignal { get; private set; }
        [Inject]
        public RestartView View { get; private set; }

        public override void OnRegister()
        {
            View.Dispatcher.UpdateListener(true, RestartView.RESTART_EVENT, OnRestart);
            View.Init();
        }
        void OnRestart()
        {
            RestartSignal.Dispatch();
        }
    }
}