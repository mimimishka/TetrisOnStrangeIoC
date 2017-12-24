using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class CellFilling
    {
        public Color Color { get; set; }
    }

    public class CellDescriptor : MonoBehaviour
    {
        [SerializeField]
        Image icon;
        public Image Icon { get { return icon; } }
        public bool Enabled { get; set; }
        CellFilling filling;
        public CellFilling Filling
        {
            get { return filling; }
            set
            {
                filling = value;
                if (Icon)
                {
                    Color newCol = filling.Color;
                    newCol.a = 0f;
                    Icon.color = newCol;
                }
            }
        }
        public Vector2 Center { get; set; }
        float minX, maxX, minY, maxY;
        void Awake()
        {
            UpdateCenter();
        }
        public void UpdateCenter()
        {
            Vector3[] corners = new Vector3[4];
            icon.rectTransform.GetWorldCorners(corners);
            minX = Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
            maxX = Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
            minY = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
            maxY = Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
            Center = new Vector2
            (
                (maxX - minX) / 2 + minX,
                (maxY - minY) / 2 + minY
            );
        }
        public bool IsUnderCell(CellDescriptor cell)
        {
            return cell.Center.x >= minX && cell.Center.x <= maxX &&
                cell.Center.y >= minY && cell.Center.y <= maxY;
        }
    }
}