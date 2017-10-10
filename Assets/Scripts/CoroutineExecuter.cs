using System.Collections;
using UnityEngine;

public interface ICoroutineExecuter
{
    void Execute(IEnumerator coroutine);
}

public class CoroutineExecuter : ICoroutineExecuter
{

    class CoroutineWorker : MonoBehaviour { }

    CoroutineWorker coroutineWorker;

    public CoroutineExecuter()
    {
        coroutineWorker = new GameObject("CoroutineWorker").AddComponent<CoroutineWorker>();
    }

    public void Execute(IEnumerator coroutine)
    {
        coroutineWorker.StartCoroutine(coroutine);
    }

}
