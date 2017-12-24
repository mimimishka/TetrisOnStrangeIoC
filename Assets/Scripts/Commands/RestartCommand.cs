using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
using UnityEngine.SceneManagement;

namespace Tetris
{
    public class RestartCommand : Command
    {
        public override void Execute()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}