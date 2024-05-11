using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;

    [SerializeField] private DialogueController dialogueController;

    public void ChangeField(int value)
    {
        Story story = new Story(inkJSON.text);

        story.variablesState["nameField"] = value;
        Debug.Log(story.variablesState["nameField"]);

        var currentStory = dialogueController.CurrentStory;

        currentStory.variablesState["nameField"] = value;
        Debug.Log(currentStory.variablesState["nameField"]);
    }
}
