using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class TextMover
    {
        float error = 3f;
        float smoothTime = .5f;
        public IEnumerator MoveText(Text text, Vector3 target)
        {
            Vector3 velocity = Vector3.zero;
            while ((text.transform.position - target).magnitude > error)
            {
                text.transform.position = Vector3.SmoothDamp
                (
                    text.transform.position,
                    target,
                    ref velocity,
                    smoothTime
                );
                yield return null;
            }
        }
    }
}