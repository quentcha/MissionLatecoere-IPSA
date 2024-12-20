using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{   
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [Header("Background management")]
    [SerializeField] private Sprite[] background;
    [SerializeField] private SpriteRenderer backgroundPanel;
    private Dictionary<string, Sprite> backgroundDictionary;
    [Header("Foreground management")]
    [SerializeField] private Sprite[] foreground;
    [SerializeField] private SpriteRenderer foregroundPanel;
    private Dictionary<string, Sprite> foregroundDictionary;

    private Story currentStory;

    private static DialogueManager instance;

    private bool dialogueIsPlaying;

    private const string SPEAKER_TAG = "speaker";
    private const string BG_TAG = "BG";
    private const string FG_TAG = "FG";

    private void Awake(){
        if (instance!=null){
            Debug.LogWarning("Found more than one dialogue manager in the scene");
        }
        instance=this;
    }

    public static DialogueManager GetInstance(){
        return instance;
    }

    private void Start(){
        backgroundDictionary = new Dictionary<string, Sprite>();
        foreach (var bgimage in background){
            backgroundDictionary.Add(bgimage.name, bgimage);
        }
        foregroundDictionary = new Dictionary<string, Sprite>();
        foreach (var fgimage in foreground){
            foregroundDictionary.Add(fgimage.name, fgimage);
        }

        currentStory = new Story(inkJSON.text);
        ContinueStory();
    }

    private void ExitDialogueMode(){
        Debug.Log("an empty Json fil was give");
        dialogueText.text="";
    }

    public void ContinueStory(){
        if(currentStory.canContinue){
            dialogueText.text = currentStory.Continue();//vérifie si on peux continuer l'histoire et si ou donne la prochaine phrase
            HandleTags(currentStory.currentTags);//gère les tags pour les noms par exemple mais on peux aussi penser a changer des images
        }
        else
        {
            ExitDialogueMode();
            SceneManager.LoadScene("SampleScene");
        }
    }
     private void HandleTags(List<string> currentTags){
        //Loop through each tag and handle it accordingly
        foreach (string tag in currentTags){
            //parse the tag
            string[] splitTag = tag.Split(':');
            //defensive programming check
            if (splitTag.Length !=2){
                Debug.LogError("Tag could not be appropriately parsed:"+tag);
            }
            string tagKey=splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            //handle the tag
            switch (tagKey){
                case SPEAKER_TAG:
                    displayNameText.text=tagValue;
                    break;
                case BG_TAG:
                    if (backgroundDictionary.ContainsKey(tagValue)){
                        backgroundPanel.sprite = backgroundDictionary[tagValue];
                    }
                    else{
                        Debug.LogWarning("no image named "+tagValue);
                    }
                    break;
                case FG_TAG:
                    if (foregroundDictionary.ContainsKey(tagValue)){
                        foregroundPanel.sprite = foregroundDictionary[tagValue];
                    }
                    else{
                        Debug.LogWarning("no image named "+tagValue);
                    }
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: "+tag);
                    break;
            }
        }
    }
}
