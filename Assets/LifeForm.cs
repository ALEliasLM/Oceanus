using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class LifeForm : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ScanEffect;
    public bool alreadyScanned = false;
    public const float scanNeed = 3;
    public float scanProgress = 0;
    public bool onScan;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(onScan && !alreadyScanned)
        {
            ScanEffect?.SetActive(true);
            scanProgress += Time.deltaTime;
            if(scanProgress >= scanNeed )
            {
                alreadyScanned = true;
                AI_Spectrum.LaraTalkShow("The clownfish, with its vibrant colors and quirky habits, is like the ocean's very own fashion disaster. It hides in anemones, thinking it's a camouflage pro, and switches homes like a commitment-phobic wanderer. Plus, it's a gender-bending drama queen, choosing when to be male or female. All in all, it's a lesson in not taking life too seriously. Embracing our inner clownfish might just be the key to a little more fun in our lives.");
            }
        }else
            ScanEffect?.SetActive(false) ;
    }
}
