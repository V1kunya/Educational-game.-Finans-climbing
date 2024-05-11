using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;

    private bool isPlayerEnter;

    private DialogueController dialogueController;
    private DialogueWindow dialogueWindow;

    private void Start()
    {
        isPlayerEnter = false;

        dialogueController = FindObjectOfType<DialogueController>();
        dialogueWindow = FindObjectOfType<DialogueWindow>();
    }

    private void Update() 
    {
        if(dialogueWindow.IsPlaying == true || isPlayerEnter == false)
            return;
        
        if  (Input.GetKeyDown(KeyCode.E))
        {
            dialogueController.EnterDialogueMode(inkJSON);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        GameObject obj = collider.gameObject;

        if (obj.GetComponent<Player>() != null)
            isPlayerEnter = true;
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        GameObject obj = collider.gameObject;

        if (obj.GetComponent<Player>() != null)
            isPlayerEnter= false;
    }
}
