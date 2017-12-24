using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;

namespace Test
{
    public class TestStartCommand : Command
    {
        public override void Execute()
        {
            Debug.Log("Start command");
        }
    }
    public class ButtonClickCommand : Command
    {
        public override void Execute()
        {
            Debug.Log("Button click");
        }
    }
}