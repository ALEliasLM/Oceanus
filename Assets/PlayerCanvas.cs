using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    // Start is called before the first frame update

    public static PlayerCanvas instance;
    Animator animator;

    public TextMeshProUGUI SpeakerText;
    public TextMeshProUGUI TBText;

    public TextMeshProUGUI OxigenText;
    public Slider OxigenSlider;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        if(instance == null)
        {
            instance = this;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator WriteText(string[] s)
    {
        animator.Play("OpenTextBox");
        yield return new WaitForSeconds(.5f);
        SpeakerText.text = s[0];
        TBText.text = s[1];
        yield break;
    }

    public void CloseText()
    {
        animator.Play("CloseTextBox");
    }
}
