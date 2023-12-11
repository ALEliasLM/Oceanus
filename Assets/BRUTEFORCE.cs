using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRUTEFORCE : MonoBehaviour
{
    public Vector3 OFFSET;
    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = OFFSET;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
