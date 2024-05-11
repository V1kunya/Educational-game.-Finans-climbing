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

    public Story CurrentStory{ get; private set; }
    private Coroutine displayLineCoroutine;

    private void Awake() 
    {
        dialogueTag = GetComponent<DialogueTag>();
        dialogueWindow = GetComponent<DialogueWindow>();

        dialogueTag.Init();
        dialogueWindow.Init();
    }

    private void Start() 
    {
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

        dialogueWindow.SetActive(true);

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(dialogueWindow.CoolDownNewLetter);

        dialogueWindow.SetActive(false);
        dialogueWindow.ClearText();
    }

    private void ContinueStory()
    {
        if (CurrentStory. canContinue == false)
        {
            StopCoroutine(ExitDialogueMode());
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
