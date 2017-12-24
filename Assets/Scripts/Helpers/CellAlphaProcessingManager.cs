using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class CellAlphaProcessingManager
    {
        [Inject]
        public IExecutor Executor { get; private set; }
        float alphaChangeStep = 1f;
        Dictionary<CellDescriptor, Coroutine> CellAlphaProcessings;
        public CellAlphaProcessingManager()
        {
            CellAlphaProcessings = new Dictionary<CellDescriptor, Coroutine>();
        }
        void StopAlphaProcessing(CellDescriptor cell)
        {
            if (CellAlphaProcessings.ContainsKey(cell))
            {
                if (CellAlphaProcessings[cell] != null)
                {
                    Executor.StopCoroutine(CellAlphaProcessings[cell]);
                }
                CellAlphaProcessings.Remove(cell);
            }
        }
        public void HighlightCell(CellDescriptor cell)
        {
            StopAlphaProcessing(cell);
            CellAlphaProcessings.Add
            (
                cell,
                Executor.ExecCoroutine
                (
                    CellAlphaProcessing(cell, 1f)
                )
            );
        }
        public void HideCell(CellDescriptor cell)
        {
            StopAlphaProcessing(cell);
            CellAlphaProcessings.Add
            (
                cell,
                Executor.ExecCoroutine
                (
                    CellAlphaProcessing(cell, 0f)
                )
            );
        }
        public void ForceHideCell(CellDescriptor cell)
        {
            StopAlphaProcessing(cell);
            ChangeCellAlpha(cell, 0f);
        }
        public void ForceHighlightCell(CellDescriptor cell)
        {
            StopAlphaProcessing(cell);
            ChangeCellAlpha(cell, 1f);
        }
        public void HighlightCells(IEnumerable<CellDescriptor> cells, bool checkIfEnabled)
        {
            foreach (CellDescriptor cell in cells)
                if (checkIfEnabled && cell.enabled || !checkIfEnabled)
                    HighlightCell(cell);
        }
        public void HideCells(IEnumerable<CellDescriptor> cells)
        {
            foreach (CellDescriptor cell in cells)
                HideCell(cell);
        }
        public void ForceHideCells(IEnumerable<CellDescriptor> cells)
        {
            foreach (CellDescriptor cell in cells)
                ForceHideCell(cell);
        }
        public void ForceHighlightCells(IEnumerable<CellDescriptor> cells, bool checkIfEnabled)
        {
            foreach (CellDescriptor cell in cells)
                if ((checkIfEnabled && cell.Enabled) || !checkIfEnabled)
                    ForceHighlightCell(cell);
        }
        IEnumerator CellAlphaProcessing(CellDescriptor cell, float target)
        {
            float current = cell.Icon.color.a;
            float stepMult = target == 1f ? 1f : -1f;
            while (current != target)
            {
                current = Mathf.Clamp(current + stepMult * alphaChangeStep * Time.deltaTime, 0f, 1f);
                ChangeCellAlpha(cell, current);
                yield return null;
            }
        }
        void ChangeCellAlpha(CellDescriptor cell, float value)
        {
            Color newColor = cell.Icon.color;
            newColor.a = value;
            cell.Icon.color = newColor;
        }
    }
}