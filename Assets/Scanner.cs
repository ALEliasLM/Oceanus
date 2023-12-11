using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{

    public List<LifeForm> onAnalisis;
    // Start is called before the first frame update
    private void OnDisable()
    {
        foreach(var f in onAnalisis)
        {
            f.onScan = false;
        }
    }

    private void OnEnable()
    {
        onAnalisis = new();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LifeForm"))
        {
            var v = other.GetComponent<LifeForm>();
            onAnalisis.Add(v);
            v.onScan = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LifeForm"))
        {
            var v = other.GetComponent<LifeForm>();
            onAnalisis.Remove(v);
            v.onScan = false;

        }
    }
}
