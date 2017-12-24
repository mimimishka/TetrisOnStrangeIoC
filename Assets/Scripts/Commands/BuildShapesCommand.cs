using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class BuildShapesCommand : Command
    {
        [Inject]
        public ShapesGenerator ShapesGenerator { get; private set; }
        [Inject]
        public CellAlphaProcessingManager CellAlphaProcManager { get; private set; }
        [Inject]
        public FillingGenerator FillGenerator { get; private set; }
        [Inject]
        public IGrid<NestDescriptor> NestGrid { get; private set; }

        public override void Execute()
        {
            foreach (NestDescriptor nest in NestGrid.Elements)
            {
                nest.AssignShape(ShapesGenerator.GenShape());
                nest.AssignFilling(FillGenerator.GenFilling());
            }
            foreach (NestDescriptor nest in NestGrid.Elements)
            {
                nest.Used = false;
                foreach (CellDescriptor cell in nest.Elements)
                    if (cell.Enabled)
                        CellAlphaProcManager.HighlightCell(cell);
            }
        }
    }
}