using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class TouchInfo
    {
        public Vector3 Pos { get; set; }
    }

    public interface ITouch
    {
        bool TouchDown { get; }
        bool TouchUp { get; }
        TouchInfo CurrentTouch { get; }
    }

    public class MouseTouch : ITouch
    {
        public bool TouchDown
        { get { return Input.GetMouseButtonDown(0); } }
        public bool TouchUp
        { get { return Input.GetMouseButtonUp(0); } }
        public TouchInfo CurrentTouch
        { get { return new TouchInfo { Pos = Input.mousePosition }; } }
    }

    public class FingerTouch : ITouch
    {
        public bool TouchDown
        { get { return (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began); } }
        public bool TouchUp
        { get { return (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended); } }
        public TouchInfo CurrentTouch
        {
            get
            {
                if (Input.touchCount == 1 &&
                    Input.touches[0].phase != TouchPhase.Ended &&
                    Input.touches[0].phase != TouchPhase.Canceled)
                    return new TouchInfo { Pos = Input.touches[0].position };
                return null;
            }
        }
    }
}