using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;

    private bool isPlayerEnter;

    private DialogueController dialogueController;

    private DialogueWindow dialogueWindow;
    
    [SerializeField] private GameObject visualCue;

    private void Start()
    {
        isPlayerEnter = false;
        visualCue.SetActive(false);
        dialogueController = FindObjectOfType<DialogueController>();
        dialogueWindow = FindObjectOfType<DialogueWindow>();
    }

    private void Update() 
    {
        if (isPlayerEnter)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
                dialogueController.EnterDialogueMode(inkJSON);
        }
        if (dialogueWindow.IsPlaying || !isPlayerEnter)
            visualCue.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        var obj = collider.gameObject;

        if (obj.tag == "Player")
            isPlayerEnter = true;
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        var obj = collider.gameObject;

        if (obj.tag == "Player")
            isPlayerEnter = false;
    }
}
