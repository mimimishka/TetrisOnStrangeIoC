using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;

namespace Tetris
{
    public class DragStartCommand : Command
    {
        [Inject]
        public NestDescriptor NestSelected { get; private set; }
        RectTransform selectedTr;
        RectTransform SelectedTr
        {
            get
            {
                if (!selectedTr)
                    selectedTr = NestSelected.GetComponent<RectTransform>();
                return selectedTr;
            }
        }
        [Inject]
        public Cursor Cursor { get; private set; }
        RectTransform cursorTr;
        RectTransform CursorTr
        {
            get
            {
                if (!cursorTr)
                    cursorTr = Cursor.GetComponent<RectTransform>();
                return cursorTr;
            }
        }
        [Inject]
        public CellAlphaProcessingManager AlphaManager { get; private set; }
        [Inject]
        public ITouch TouchInput { get; private set; }
        [Inject]
        public IExecutor Executor { get; private set; }
        [Inject]
        public NestDropSignal DropSignal { get; private set; }

        public override void Execute()
        {
            if (NestSelected.Used)
                return;
            Cursor.Nest.AssignShape(NestSelected.Shape);
            Cursor.Nest.AssignFilling(NestSelected.Filling);
            AlphaManager.ForceHideCells(Cursor.Nest.Elements);
            AlphaManager.ForceHighlightCells(Cursor.Nest.Elements, true);
            CursorTr.position = SelectedTr.position;
            AlphaManager.ForceHideCells(NestSelected.Elements);
            Cursor.NestSelected = NestSelected;
            Executor.ExecCoroutine(ChasePointer());
        }
        IEnumerator ChasePointer()
        {
            Vector3 offset = CursorTr.position - TouchInput.CurrentTouch.Pos;
            while(!TouchInput.TouchUp)
            {
                CursorTr.position = TouchInput.CurrentTouch.Pos + offset;
                foreach (CellDescriptor cell in Cursor.Nest.Elements)
                    cell.UpdateCenter();
                yield return null;
            }
            DropSignal.Dispatch();
        }
    }
}