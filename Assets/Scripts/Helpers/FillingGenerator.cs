using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class FillingGenerator : MonoBehaviour
    {
        [SerializeField]
        List<Color> colors;
        CellFilling def;
        public CellFilling Default
        {
            get
            {
                if(def == null)
                    def = new CellFilling { Color = Color.white };
                return def;
            }
        }
        public CellFilling GenFilling()
        {
            return new CellFilling { Color = colors[Random.Range(0, colors.Count - 1)] };
        }
    }
}