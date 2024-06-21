using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float moveSpeed;

    private Rigidbody2D myRigidbody;

    private Vector3 direction;

    private Animator animator;

    void Start() 
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() 
    {
        direction = Vector3.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        if (DialogueController.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove()
    {
        if(direction != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
            animator.SetBool("isMoving", true);
        }
        else
            animator.SetBool("isMoving", false);
    }
    
    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
    }
}
