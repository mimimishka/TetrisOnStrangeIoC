using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;

namespace Tetris
{
    public class EnableDetectorsCommand : Command
    {
        [Inject]
        public Cursor Cursor { get; private set; }
        public override void Execute()
        {
            foreach (ProtectorView protector in GameObject.FindObjectsOfType<ProtectorView>())
                protector.StartDetect();
            Cursor.Nest.Container = Cursor.transform;
        }
    }
}