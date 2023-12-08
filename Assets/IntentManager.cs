using Meta.WitAi.Json;
using Oculus.Voice;
using Oculus.Voice.Dictation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentManager : MonoBehaviour
{
    AppVoiceExperience voiceRecognizer;
    AppDictationExperience DRecognizer;
    // Start is called before the first frame update
    void Start()
    {
        
        voiceRecognizer = GetComponent<AppVoiceExperience>();
        DRecognizer = GetComponent<AppDictationExperience>();

        voiceRecognizer.VoiceEvents.OnPartialResponse.AddListener(DetectIntent);
        DRecognizer.DictationEvents.OnResponse.AddListener(DetectIntent);
        DRecognizer.DictationEvents.OnFullTranscription.AddListener((string s) => print(s));
    }

    private void DetectIntent(WitResponseNode response)
    {
        print("chegou algo");
        print(response.ToString());
        string intentName = response["intents"][0]["name"].Value.ToString();
        print(intentName);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            voiceRecognizer.Activate();
        }
    }
}
