using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PullFish : MonoBehaviour
{
    [SerializeField] float pullForce = 10;
    [SerializeField] Transform point;
    List<Rigidbody> commonFishList;

    Collider collider;


    private void OnEnable()
    {

        commonFishList= new List<Rigidbody>();
    }

    private void OnDisable()
    {
        commonFishList = null;
    }

    
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (commonFishList == null || commonFishList.Count == 0) return;
        foreach(Rigidbody f in commonFishList)
        {
            //f.velocity = Vector3.zero;
            //f.angularVelocity = Vector3.zero;
            if(f == null) { 

                continue; 
            }
            f.AddForce((point.position - f.transform.position).normalized * pullForce / f.mass, ForceMode.Acceleration );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<CommonFish>(out CommonFish hasCommonFish);
        commonFishList.Add(hasCommonFish?.GetComponent<Rigidbody>());
    }

    private void OnTriggerExit(Collider other)
    {
        if(commonFishList == null) return;
        other.TryGetComponent(out CommonFish hasCommonFish);
        var rb = hasCommonFish?.GetComponent<Rigidbody>();
        if (commonFishList.Contains(rb)) commonFishList.Remove(rb);
    }
}
