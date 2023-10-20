using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PullFish : MonoBehaviour
{
    public TextMeshProUGUI m_TextMeshPro;
    [SerializeField] float pullForce = 10;
    [SerializeField] Transform point;
    List<Rigidbody> commonFishList;

    private void OnEnable()
    {
        m_TextMeshPro.text = "0";
        commonFishList= new List<Rigidbody>();
    }

    private void OnDisable()
    {
        m_TextMeshPro.text = "0";
        commonFishList = null;
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (commonFishList == null || commonFishList.Count == 0) return;
        m_TextMeshPro.text = commonFishList.Count.ToString();
        foreach(Rigidbody f in commonFishList)
        {
            //f.velocity = Vector3.zero;
            //f.angularVelocity = Vector3.zero;
            f.AddForce((point.position - f.transform.position).normalized * pullForce, ForceMode.Acceleration);
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
        other.TryGetComponent<CommonFish>(out CommonFish hasCommonFish);
        var rb = hasCommonFish?.GetComponent<Rigidbody>();
        if (commonFishList.Contains(rb)) commonFishList.Remove(rb);
    }
}
