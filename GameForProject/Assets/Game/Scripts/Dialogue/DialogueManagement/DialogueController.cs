using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Unity.VisualScripting;

[RequireComponent(typeof(DialogueWindow), typeof(DialogueTag))]
public class DialogueController : MonoBehaviour
{
    private DialogueWindow dialogueWindow;
    private DialogueTag dialogueTag;
    public bool dialogueIsPlaying { get; private set; }    
    public Story CurrentStory{ get; private set; }
    private Coroutine displayLineCoroutine;
    private static DialogueController instance;

    private void Awake() 
    {
        if (instance != null)
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        instance = this;
        dialogueTag = GetComponent<DialogueTag>();
        dialogueWindow = GetComponent<DialogueWindow>();

        dialogueTag.Init();
        dialogueWindow.Init();

    }

    public static DialogueController GetInstance()
    {
        return instance;
    }

    private void Start() 
    {
        dialogueIsPlaying = false;
        dialogueWindow.SetActive(false);
    }

    private void Update() 
    {
        if ( dialogueWindow.IsStatusAnswer == true || dialogueWindow.IsPlaying == false || dialogueWindow.CanCountinueToNextLine == false)
            return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            ContinueStory();
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        CurrentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialogueWindow.SetActive(true);
        ContinueStory();
    }

    public IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(dialogueWindow.CoolDownNewLetter);
        dialogueIsPlaying = false;
        dialogueWindow.SetActive(false);
        dialogueWindow.ClearText();
    }

    private void ContinueStory()
    {
        if (CurrentStory.canContinue == false)
        {
            StartCoroutine(ExitDialogueMode());
            return;
        }

        if (displayLineCoroutine != null)
            StopCoroutine(displayLineCoroutine);

        displayLineCoroutine = StartCoroutine(dialogueWindow.DisplayLine(CurrentStory));

        try
        {
            dialogueTag.HandleTags(CurrentStory.currentTags);
        }

        catch (ArgumentException ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public void MakeChoise(int choiceIndex)
    {
        dialogueWindow.MakeChoise();
        CurrentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }
}
