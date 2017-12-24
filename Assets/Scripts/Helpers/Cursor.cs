using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris {
    public class Cursor : MonoBehaviour
    {
        [SerializeField]
        NestDescriptor nest;
        public NestDescriptor Nest { get { return nest; } }
        public NestDescriptor NestSelected { get; set; }
    }
}