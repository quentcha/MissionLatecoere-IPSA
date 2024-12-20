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
    [SerializeField] private Image backgroundPanel;
    private Dictionary<string, Sprite> backgroundDictionary;

    private Story currentStory;

    private static DialogueManager instance;

    private bool dialogueIsPlaying;

    private const string SPEAKER_TAG = "speaker";
    private const string IMAGE_TAG = "image";

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
        foreach (var image in background){
            backgroundDictionary.Add(image.name, image);
        }

        currentStory = new Story(inkJSON.text);
        ContinueStory();
    }

    private void Update(){
        //if (InputManager.GetInstance().GetSubmitPressed()){
          //  ContinueStory();
        //}
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
                case IMAGE_TAG:
                    if (backgroundDictionary.ContainsKey(tagValue)){
                        backgroundPanel.sprite = backgroundDictionary[tagValue];
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