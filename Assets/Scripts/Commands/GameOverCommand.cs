using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using strange.extensions.command.impl;

namespace Tetris
{
    public class GameOverCommand : Command
    {
        [Inject(name = TextType.GAMEOVER)]
        public ITextAccessor GameOverTextAcc { get; private set; }
        Text GameOverText { get { return GameOverTextAcc.Text; } }
        [Inject]
        public TextMover TextMover { get; private set; }
        [Inject]
        public IExecutor Executor { get; private set; }
        [Inject(name = MotionType.HORIZONTAL)]
        public IPositionsSetAccessor PosAccessor { get; private set; }
        [Inject]
        public ButtonAccessor RestartButtonGO { get; private set; }

        public override void Execute()
        {
            Executor.ExecCoroutine(ShowGameOver());
        }
        IEnumerator ShowGameOver()
        {
            GameOverText.transform.position = PosAccessor.StartP;
            GameOverText.text = "GAME OVER";
            yield return TextMover.MoveText(GameOverText, PosAccessor.MiddleP);
            yield return new WaitForSeconds(1f);
            RestartButtonGO.Button.gameObject.SetActive(true);
        }
    }
}