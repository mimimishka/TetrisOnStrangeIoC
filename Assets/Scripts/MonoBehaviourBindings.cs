using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Tetris
{
    public class ButtonAccessor
    {
        public Button Button { get; set; }
    }
    public class MonoBehaviourBindings : MonoBehaviour
    {
        [SerializeField]
        Text startText;
        public Text StartText { get { return startText; } }
        [SerializeField]
        Transform verticalPosSet;
        public Transform VerticalPosSet { get { return verticalPosSet; } }
        [SerializeField]
        Transform cellsGrid;
        public Transform CellsGrid { get { return cellsGrid; } }
        [SerializeField]
        Transform nestsGrid;
        public Transform NestsGrid { get { return nestsGrid; } }
        [SerializeField]
        Cursor cursor;
        public Cursor Cursor { get { return cursor; } }
        [SerializeField]
        FillingGenerator fillGenerator;
        public FillingGenerator FillGenerator { get { return fillGenerator; } }
        [SerializeField]
        Text gameOverText;
        public Text GameOverText { get { return gameOverText; } }
        [SerializeField]
        Transform horizontalPosSet;
        public Transform HorizontalPosSet { get { return horizontalPosSet; } }
        [SerializeField]
        Button restartButton;
        public Button RestartButton { get { return restartButton; } }
    }
}