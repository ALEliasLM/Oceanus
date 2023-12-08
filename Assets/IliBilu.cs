using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IliBilu : MonoBehaviour
{

    private float maxOxigen = 60;
    public float currentOxigen = 60;
    public bool onSafeZone = true;

    private Slider slider;
    private TextMeshProUGUI sliderText;
    void Start()
    {
        slider = PlayerCanvas.instance.OxigenSlider;
        sliderText = PlayerCanvas.instance.OxigenText;
        StartCoroutine(OxigenCicle());
    }

    public IEnumerator OxigenCicle()
    {
        while(true)
        {
            if (onSafeZone)
            {
                if (currentOxigen < maxOxigen)
                {
                    currentOxigen++;
                    yield return null;
                }
            }
            else
            {
                if(currentOxigen > 0)
                {
                    yield return new WaitForSeconds(3);
                    currentOxigen -= 3;
                }
                else
                {
                    //apagar a tela. sla.
                }
                
            }
            slider.value = currentOxigen / maxOxigen * 100;
            sliderText.text = currentOxigen.ToString() +"s";
            yield return null;
        }
    }
}
