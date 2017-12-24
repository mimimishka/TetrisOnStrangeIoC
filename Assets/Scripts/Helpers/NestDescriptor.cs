using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class NestDescriptor : MonoBehaviour, IGrid<CellDescriptor>
    {
        public Transform Container { set { Init(value); } }
        List<CellDescriptor> elements;
        public List<CellDescriptor> Elements { get { return elements; } }
        public Shape Shape { get; private set; }
        public CellFilling Filling { get; private set; }
        void Init(Transform container)
        {
            elements = new List<CellDescriptor>();
            for (int i = 0; i < container.childCount; ++i)
                elements.Add(container.GetChild(i).GetComponent<CellDescriptor>());
        }
        public void AssignShape(Shape shape)
        {
            Shape = shape;
            for (int i = 0; i < shape.Mask.Length; ++i)
                Elements[i].Enabled = shape.Mask[i];
        }
        public void AssignFilling(CellFilling filling)
        {
            Filling = filling;
            foreach (CellDescriptor cell in Elements)
                cell.Filling = filling;
        }
        public bool Used { get; set; }
    }
}