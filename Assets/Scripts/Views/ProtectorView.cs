using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Tetris
{
    public class ProtectorView : View
    {
        [SerializeField]
        NestDescriptor nest;
        public NestDescriptor Nest { get { return nest; } }
        //public NestDescriptor Nest { get { return nest; } }
        internal const string TAP_EVENT = "tap event";

        void OnTapDown()
        {
            dispatcher.Dispatch(TAP_EVENT);
        }

        [Inject]
        public ITouch TouchInput { get; private set; }
        [Inject]
        public IEventDispatcher dispatcher { get; set; }

        float minX, maxX, minY, maxY;
        internal void Init()
        {
            RectTransform rTr = GetComponent<RectTransform>();
            Vector3[] corners = new Vector3[4];
            rTr.GetWorldCorners(corners);
            minX = Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
            maxX = Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
            minY = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
            maxY = Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
        }
        public void StartDetect()
        {
            StartCoroutine(ClickWatching());
        }
        IEnumerator ClickWatching()
        {
            while (true)
            {
                if (TouchInput.TouchDown)
                {
                    Vector3 touchPos = TouchInput.CurrentTouch.Pos;
                    if (touchPos.x >= minX && touchPos.x <= maxX &&
                    touchPos.y >= minY && touchPos.y <= maxY)
                    {
                        OnTapDown();
                    }
                }
                yield return null;
            }
        }
    }
}