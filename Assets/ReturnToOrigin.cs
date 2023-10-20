using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToOrigin : MonoBehaviour
{
    // Start is called before the first frame update

    public Coroutine actualCoroutine;
    public Vector3 originalRot;


    public void SetMove(bool grabbed)
    {
        if (grabbed)
        {
            StopAllCoroutines();
            actualCoroutine = null;
        }
        else
        {
            actualCoroutine = StartCoroutine(Return());
        }
    }

    IEnumerator Return()
    {
        while((transform.localEulerAngles - originalRot).magnitude > 1)
        {
            var rot = Vector3.Lerp(transform.localEulerAngles, originalRot, Time.deltaTime);
            transform.localEulerAngles = rot;
            yield return null;
        }
    }
}
