using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Cell
    {
        public bool Filled { get; set; }
        public CellFilling Filling { get; set; }
    }
}