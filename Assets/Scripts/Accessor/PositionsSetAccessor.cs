using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public interface IPositionsSetAccessor
    {
        Vector3 StartP { get; }
        Vector3 MiddleP { get; }
        Vector3 FinishP { get; }
    }

    public class PositionsSetAccessor : IPositionsSetAccessor
    {
        bool inited = false;
        Transform container;
        public Transform Container
        {
            get { return container; }
            set
            {
                container = value;
                Init();
            }
        }
        Vector3 start, middle, finish;
        public Vector3 StartP
        {
            get
            {
                if (!inited)
                    Init();
                return start;
            }
        }
        public Vector3 MiddleP
        {
            get
            {
                if (!inited)
                    Init();
                return middle;
            }
        }
        public Vector3 FinishP
        {
            get
            {
                if (!inited)
                    Init();
                return finish;
            }
        }
        void Init()
        {
            start = Container.GetChild(0).position;
            middle = Container.GetChild(1).position;
            finish = Container.GetChild(2).position;
            inited = true;
        }
    }
}