using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScannerGoBRRR : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Vector3 onSearchShape;
    [SerializeField] Vector3 onFindShape;
    [SerializeField] Transform scannerOrigin;
    [SerializeField] Vector2 angleCap;

    private List<LifeForm> lifeFormsOnScan = new();

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<LifeForm>(out var has);
        if (has  != null && !(has.alreadyScanned))
        {
            lifeFormsOnScan.Add(has);
            has.onScan = true;
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}
