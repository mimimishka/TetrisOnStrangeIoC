using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public interface IExecutor
    {
        Coroutine ExecCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine cor);
    }

    public class CoroutineWorker : MonoBehaviour
    { }

    public class Executor : IExecutor
    {
        CoroutineWorker worker;
        public Executor()
        {
            GameObject go = new GameObject("CoroutineWorker");
            worker = go.AddComponent<CoroutineWorker>();
        }
        public Coroutine ExecCoroutine(IEnumerator coroutine)
        {
            return worker.StartCoroutine(coroutine);
        }
        public void StopCoroutine(Coroutine cor)
        {
            if (cor != null)
                worker.StopCoroutine(cor);
        }
    }
}