using strange.extensions.command.impl;
using strange.extensions.command.api;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.injector.impl;

namespace Tetris
{
    public class ShowGreetingsCommand : Command
    {
        [Inject]
        public CellAlphaProcessingManager CellAlphaProcManager { get; private set; }
        [Inject]
        public MainModel Model { get; private set; }
        [Inject]
        public IExecutor Executor { get; private set; }
        [Inject(name = TextType.START)]
        public ITextAccessor StartTextAccessor { get; private set; }
        Text StartText { get { return StartTextAccessor.Text; } }
        [Inject(name = MotionType.VERTICAL)]
        public IPositionsSetAccessor PositionsSetAccessor { get; private set; }
        [Inject]
        public IGrid<CellDescriptor> FieldGrid { get; private set; }
        [Inject]
        public IGrid<NestDescriptor> NestGrid { get; private set; }
        [Inject]
        public GameStartedSignal GameStartSignal { get; private set; }
        [Inject]
        public TextMover TextMover { get; private set; }


        public override void Execute()
        {
            Executor.ExecCoroutine(ShowGreetings());
        }
        IEnumerator ShowGreetings()
        {
            ForceHide();
            StartText.transform.position = PositionsSetAccessor.StartP;
            StartText.text = "Let's TETRIS!";
            HighlighAll();
            yield return TextMover.MoveText(StartText, PositionsSetAccessor.MiddleP);
            HideAll();
            yield return TextMover.MoveText(StartText, PositionsSetAccessor.FinishP);
            StartText.text = "";
            GameStartSignal.Dispatch();
        }
        
        void HideAll()
        {
            foreach (CellDescriptor cell in FieldGrid.Elements)
                CellAlphaProcManager.HideCell(cell);
            foreach (NestDescriptor nest in NestGrid.Elements)
                foreach (CellDescriptor cell in nest.Elements)
                    CellAlphaProcManager.HideCell(cell);
        }
        void HighlighAll()
        {
            foreach (CellDescriptor cell in FieldGrid.Elements)
                CellAlphaProcManager.HighlightCell(cell);
            foreach (NestDescriptor nest in NestGrid.Elements)
                foreach (CellDescriptor cell in nest.Elements)
                    CellAlphaProcManager.HighlightCell(cell);
        }
        void ForceHide()
        {
            foreach (CellDescriptor cell in FieldGrid.Elements)
                CellAlphaProcManager.ForceHideCell(cell);
            foreach (NestDescriptor nest in NestGrid.Elements)
                foreach (CellDescriptor cell in nest.Elements)
                    CellAlphaProcManager.ForceHideCell(cell);
        }
    }
}