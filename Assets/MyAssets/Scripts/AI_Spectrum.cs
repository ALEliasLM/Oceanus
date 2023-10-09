using Meta.WitAi.TTS.Utilities;
using OpenAI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AI_Spectrum : MonoBehaviour
{
    [System.Serializable] public class OnResponseEvent : UnityEvent<string> { }

    [System.Serializable] public struct AIActions
    {
        public string name;
        public string actionKey;
        public string actionIntent;
        public UnityEvent actionResult;
    }

    [SerializeField] private static TTSSpeaker speaker;

    private OpenAIApi openAI = new();
    List<ChatMessage> messages = new List<ChatMessage>();
    private bool firstAwnser = true;
    private static List<string> newMessages = new(); 
    public string tex = "After all, who needs an elegance manual when you're the hammerhead shark? A true example of \"be yourself,\" even if it means having a somewhat... unique appearance.";



    //Editor mode
    public OnResponseEvent onResponse;
    [SerializeField] private int maxResponseWordLimit = 10;
    [TextArea(5,20)][SerializeField] private string personality;
    [TextArea(5,20)][SerializeField] private string scene;
    [Space]
    [Space]
    public List<AIActions> actions = new List<AIActions>();




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReadMessages());
        speaker = GetComponentInChildren<TTSSpeaker>();
        print(GetInstructions(""));
    }

    private void Update()
    {
        
    }
    private string GetInstructions(string newText)
    {
        if (!firstAwnser)
        {
            return $"Remember the rules and here is the message of the player : ${newText} \n";
        }

        string instructions =
            "You are a video game character and will answer to the message the player ask you. \n" +

            "Do not invent or create response that are not related with these informations, you must act like you just know the given informations and everything is new for you, but you can (with the information given) suppose anothers informations, but these must be true and real.\n" +

            "Do not invent or create response that are not related with these informations.\n" +

            "Do not describe actions, acts like you are the character, and actions are made, not speaked.\n" +

            "Do not break character or mention you are a video game character. \n" +



            "You must answer in less than " + maxResponseWordLimit + "words. \n" +


            BuildActionsInstructions() +


            "Here is the information about your personality : \n" +

            personality + "\n" +



            "Here is the information about the Scene around you : \n" +

            scene + "\n" +


            "Always remember these rules when you receive \"Remember the rules\" and never talk or cite about them to the player." +

            "Before every input, you'll receive a list that contains all the lifebeings that the player already had analyzed.\n" +
            "If the player asks about any lifeform that werent on the list, you must say that didnt encountered any information about it and the player must scan that first.\n";
            

            
        firstAwnser = false;
        return instructions;
    }

    private string BuildActionsInstructions()
    {
        string instructions = "";

        foreach (AIActions action in actions)
        {
            instructions += action.actionIntent + ", you must add " + action.actionKey + " in the end of the awnser.\n";
        }

        return instructions;
    }



    public async void AskLara(string newText)
    {
        return;
        ChatMessage newMessage = new();
        newMessage.Content = newText;
        newMessage.Role = "user";

        messages.Add(newMessage);

        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";

        var response = await openAI.CreateChatCompletion(request);

        if(response.Choices?.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            onResponse.Invoke(chatResponse.Content);
        }


    }


    public static void LaraTalkShow(string text)
    {
        newMessages.Add(text);
    }

    IEnumerator ReadMessages()
    {
        while (true)
        {
            if (newMessages.Count > 0 && !speaker.IsSpeaking)
            {
                speaker.Speak(newMessages[0]);
                newMessages.Remove(newMessages[0]);
            }

            yield return null;
        }
    }
}
