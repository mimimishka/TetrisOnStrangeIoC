using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace Tetris
{
    public class ProtectorMediator : Mediator
    {
        [Inject]
        public ProtectorView view { get; set; }

        [Inject]
        public NestTouchSignal touchSignal { get; set; }

        public override void OnRegister()
        {
            view.Init();
            view.dispatcher.UpdateListener(true, ProtectorView.TAP_EVENT, OnProtectorTap);
        }
        void OnProtectorTap()
        {
            touchSignal.Dispatch(view.Nest);
        }
    }
}