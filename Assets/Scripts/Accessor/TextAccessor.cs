using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public interface ITextAccessor
    {
        Text Text { get; }
    }
    public class TextAccessor : ITextAccessor
    {
        public Text Text { get; set; }
    }
}