using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;

namespace Tetris {
    public class ValidateFieldCommand : Command
    {
        [Inject]
        public GameOverSignal GameOverSignal { get; private set; }
        [Inject]
        public IGrid<NestDescriptor> NestGrid { get; private set; }
        [Inject]
        public MainModel Model { get; private set; }

        public override void Execute()
        {
            //Debug.Log("validate");
            if (!IsAnyNestMountable())
                GameOverSignal.Dispatch();
        }
        bool IsAnyNestMountable()
        {
            foreach(NestDescriptor nest in NestGrid.Elements)
            {
                if (nest.Used)
                    continue;
                if (IsMountable(nest))
                    return true;
            }
            return false;
        }
        bool IsMountable(NestDescriptor nest)
        {
            bool[] shape = nest.Shape.Mask;
            for (int x = -2; x < Model.Cells.GetLength(0) + 2; ++x)
            {
                for(int y = -2; y < Model.Cells.GetLength(1) + 2; ++y)
                    if (IsShapeValidOnCoords(shape, x, y))
                        return true;
            }
            return false;
        }
        bool IsShapeValidOnCoords(bool[] shape, int x, int y)
        {
            int ceilX = x + 3,
                ceilY = y + 3;
            for (int i = 0; x < ceilX; ++x, ++i)
                for (int j = 0, Y = y; Y < ceilY; ++Y, ++j)
                    if (shape[i + j * 3])
                    {
                        //Debug.Log("x: " + x + " y: " + Y);
                        if (!xValid(x) || !yValid(Y) || Model.Cells[x, Y].Filled)
                        {
                            //Debug.Log("break");
                            return false;
                        }
                    }
            return true;
        }
        bool xValid(int x) { return x >= 0 && x < Model.Cells.GetLength(0); }
        bool yValid(int y) { return y >= 0 && y < Model.Cells.GetLength(1); }
    }
}