using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public interface IGrid<T>
    {
        Transform Container { set; }
        List<T> Elements { get; }
    }
    public class GridAccessor<T> : IGrid<T>
    {
        public Transform Container { set { Init(value); } }
        List<T> elements;
        public List<T> Elements { get { return elements; } }
        void Init(Transform container)
        {
            elements = new List<T>();
            for (int i = 0; i < container.childCount; ++i)
                elements.Add(container.GetChild(i).GetComponent<T>());
        }
    }
}