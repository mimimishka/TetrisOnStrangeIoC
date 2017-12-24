using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;

namespace Tetris
{
    public class DropCursorCommand : Command
    {
        [Inject]
        public IGrid<CellDescriptor> CellGrid { get; private set; }
        [Inject]
        public Cursor Cursor { get; private set; }
        [Inject(name =SizeType.FIELD)]
        public int FieldSize { get; private set; }
        [Inject]
        public CellAlphaProcessingManager AlphaManager { get; private set; }
        [Inject]
        public MainModel Model { get; private set; }
        [Inject]
        public NestsOverSignal OverSignal { get; private set; }
        [Inject]
        public IGrid<NestDescriptor> NestGrid { get; private set; }
        //[Inject]
        //public ValidateFieldSignal ValidateSignal { get; private set; }

        public override void Execute()
        {
            Retain();
            Dictionary<CellDescriptor, CellFilling> cellsToFill = new Dictionary<CellDescriptor, CellFilling>();
            List<Vector2> coords = new List<Vector2>();
            foreach(CellDescriptor cell in Cursor.Nest.Elements)
            {
                if (!cell.Enabled)
                    continue;
                int x, y;
                CellDescriptor nearestCell = GetNearestCell(cell, out x, out y);
                if(cellsToFill.ContainsKey(nearestCell) || nearestCell.Enabled || !nearestCell.IsUnderCell(cell))
                {
                    Revoke();
                    return;
                }
                cellsToFill.Add(nearestCell, cell.Filling);
                coords.Add(new Vector2(x, y));
            }
            Cursor.NestSelected.Used = true;
            AlphaManager.ForceHideCells(Cursor.Nest.Elements);
            int i = 0;
            foreach (CellDescriptor cell in cellsToFill.Keys)
            {
                Model.Cells[(int)coords[i].x, (int)coords[i].y].Filling =
                cell.Filling = cellsToFill[cell];
                Model.Cells[(int)coords[i].x, (int)coords[i].y].Filled =
                cell.Enabled = true;
                i++;
            }
            AlphaManager.HighlightCells(cellsToFill.Keys, false);
            foreach (NestDescriptor nest in NestGrid.Elements)
                if (!nest.Used)
                {
                    Release();
                    //ValidateSignal.Dispatch();
                    return;
                }
            OverSignal.Dispatch();
            //ValidateSignal.Dispatch();
            Release();
        }
        void Revoke()
        {
            AlphaManager.ForceHideCells(Cursor.Nest.Elements);
            AlphaManager.ForceHighlightCells(Cursor.NestSelected.Elements, true);
            Fail();
        }
        CellDescriptor GetNearestCell(CellDescriptor toCell, out int x, out int y)
        {
            float previous;
            float current = float.MaxValue;
            for(x = 0; x < FieldSize; ++x)
            {
                previous = current;
                current = Mathf.Abs(toCell.Center.x - CellGrid.Elements[x].Center.x);
                if (previous < current)
                    break;
            }
            --x;
            current = float.MaxValue;
            for(y = 0; y < FieldSize; ++y)
            {
                previous = current;
                current = Mathf.Abs(toCell.Center.y - CellGrid.Elements[y * FieldSize].Center.y);
                if (previous < current)
                    break;
            }
            --y;
            return CellGrid.Elements[x + y * FieldSize];
        }
    }
}