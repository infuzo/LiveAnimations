using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apex.Mecanim;

public class TestMoving : MonoBehaviour
{

    Vector3 currentDirection;

    Coroutine coroutineChangingLine;

    private void Start()
    {
        currentDirection = Vector3.left * 5f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeLine(true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeLine(false);
        }

        GetComponent<MecanimMoverComponentComplete>().Move(currentDirection, Time.deltaTime);
    }

    void ChangeLine(bool leftOrRight)
    {
        if (coroutineChangingLine != null)
        {
            StopCoroutine(coroutineChangingLine);
        }
        coroutineChangingLine = StartCoroutine(CoroutineChangingLine(leftOrRight));
    }

    IEnumerator CoroutineChangingLine(bool leftOrRight)
    {
        GetComponent<MecanimMoverComponentComplete>().Stop();
        float targetX = leftOrRight ? -5f : 5f;
        currentDirection.z = 0f;
        while (Mathf.Abs(currentDirection.x) < Mathf.Abs(targetX))
        {
            currentDirection.x = Mathf.MoveTowards(currentDirection.x, targetX, Time.deltaTime * 10f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1f);
        while (Mathf.Abs(currentDirection.x) > 0f)
        {
            currentDirection.x = Mathf.MoveTowards(currentDirection.x, 0f, Time.deltaTime * 10f);
            yield return new WaitForEndOfFrame();
        }
        currentDirection.z = 10f;
    }

}
