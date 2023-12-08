using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycler : MonoBehaviour
{
    public HingeJoint joint;
    public Transform destructPoint;
    public ParticleSystem sparks;
    public AudioSource audio;
    Coroutine currentCoroutine;

    Vector3 startPos;
    void Start()
    {
        sparks.Stop();
        startPos = joint.transform.localEulerAngles;
        print(startPos);
    }

    // Update is called once per frame
    void Update()
    {
        //print(joint.angle);
        if (Mathf.Abs(joint.angle) >= Mathf.Abs(joint.limits.min) - 5
            && currentCoroutine == null)
        {
            //print("FUNCAO");
            currentCoroutine = StartCoroutine(Recycle());
            joint.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
        }
    }

    public IEnumerator Recycle()
    {
        var Objects = Physics.OverlapSphere(destructPoint.position, .5f);
        if (Objects.Length > 0)
        {
            sparks.Play();
            audio.Play();
            foreach (var Object in Objects)
            {
                if (Object.CompareTag("Trash")) Destroy(Object);
                yield return new WaitForSeconds(1.0f);
            }
            while (audio.isPlaying) yield return null;
            joint.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            sparks.Stop();
            
            while(Vector3.Distance(startPos, joint.transform.localEulerAngles) > .5f)
            {
                Vector3 actual = new Vector3(joint.transform.localEulerAngles.x, 0 ,0);

                joint.transform.localEulerAngles = Vector3.Lerp(actual, startPos, Time.deltaTime * 10);
                yield return new WaitForSeconds(1);
            }
            
            currentCoroutine = null;
        }
    }
}
