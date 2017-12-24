using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

namespace Tetris
{
    public class InitFieldCommand : Command
    {
        [Inject]
        public MainModel Model { get; set; }
        [Inject]
        public FillingGenerator FillGenerator { get; private set; }
        [Inject(name = SizeType.FIELD)]
        public int Size { get; private set; }
        public override void Execute()
        {
            Cell[,] cells = new Cell[Size, Size];
            for (int i = 0; i < cells.GetLength(0); ++i)
                for (int j = 0; j < cells.GetLength(1); ++j)
                    cells[i, j] = new Cell
                    {
                        Filled = false,
                        Filling = FillGenerator.Default
                    };
            Model.Cells = cells;
        }
    }
}