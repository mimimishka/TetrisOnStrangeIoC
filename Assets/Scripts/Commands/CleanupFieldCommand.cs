using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;

namespace Tetris
{
    public class CleanupFieldCommand : Command
    {
        [Inject]
        public MainModel Model { get; private set; }
        [Inject]
        public IGrid<CellDescriptor> CellGrid { get; private set; }
        [Inject(name = SizeType.FIELD)]
        public int FieldSize { get; private set; }
        [Inject]
        public CellAlphaProcessingManager AlphaManager { get; private set; }

        public override void Execute()
        {
            List<int> Xs = new List<int>();
            List<int> Ys = new List<int>();
            for(int x = 0; x < Model.Cells.GetLength(0); ++x)
            {
                bool filled = true;
                for (int y = 0; y < Model.Cells.GetLength(1); ++y)
                    if (!Model.Cells[x, y].Filled)
                    {
                        filled = false;
                        break;
                    }
                if (!filled)
                    continue;
                Xs.Add(x);
            }
            for(int y = 0; y < Model.Cells.GetLength(1); ++y)
            {
                bool filled = true;
                for (int x = 0; x < Model.Cells.GetLength(0); ++x)
                    if (!Model.Cells[x, y].Filled)
                    {
                        filled = false;
                        break;
                    }
                if (!filled)
                    continue;
                Ys.Add(y);
            }
            Cleanup(Xs, Ys);
        }
        void Cleanup(List<int> Xs, List<int> Ys)
        {
            foreach (int x in Xs)
                for (int y = 0; y < Model.Cells.GetLength(1); ++y)
                {
                    Model.Cells[x, y].Filled =
                    CellGrid.Elements[x + y * FieldSize].Enabled = false;
                }
            foreach (int y in Ys)
                for (int x = 0; x < Model.Cells.GetLength(0); ++x)
                {
                    Model.Cells[x, y].Filled = false;
                    CellGrid.Elements[x + y * FieldSize].Enabled = false;
                }
            foreach (CellDescriptor cell in CellGrid.Elements)
                if (!cell.Enabled)
                    AlphaManager.HideCell(cell);
        }
    }
}