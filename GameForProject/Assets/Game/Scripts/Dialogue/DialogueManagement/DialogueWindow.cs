using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;

[RequireComponent(typeof(DialogueChoice))]

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayName;

    [SerializeField] private TextMeshProUGUI displayText;

    [SerializeField] private GameObject dialogueWindow;

    [SerializeField, Range(0f, 20f)] private float cooldownNewLetter;

    private DialogueChoice dialogueChoice;

    public bool IsStatusAnswer { get; private set; }
    
    public bool IsPlaying { get; private set; }
    
    public bool CanCountinueToNextLine { get; private set; }

    public float CoolDownNewLetter 
    { 
        get => cooldownNewLetter; 
        private set
        {
            cooldownNewLetter = CheckCooldown(value);
        }
    }

    public float CheckCooldown(float value)
    { 
        if (value < 0)
            throw new ArgumentException(message: "Значение задержки между буквами было отрицательное");
        return value;
    }

    public void Init()
    {
        IsStatusAnswer = false;
        CanCountinueToNextLine = false;
        dialogueChoice = GetComponent<DialogueChoice>();
        dialogueChoice.Init();
    }

    public void SetActive(bool active)
    {
        IsPlaying = active;
        dialogueWindow.SetActive(active);
    }

    public void SetText(string text)
    {
        displayText.text = text;
    }

    public void Add(string text)
    {
        displayText.text += text;
    }

    public void Add(char letter)
    {
        displayText.text += letter;
    }

    public void ClearText()
    {
        SetText("");
    }

    public void SetName(string namePerson)
    {
        displayName.text = namePerson;
    }

    public void SetCoolDown(float cooldown)
    {
        cooldownNewLetter = cooldown;
    }

    public void MakeChoise()
    {
        if (CanCountinueToNextLine == false)
            return;

        IsStatusAnswer = false;
    }

    public IEnumerator DisplayLine(Story story)
    {
        var line = story.Continue();

        ClearText();
        dialogueChoice.HideChoices();

        CanCountinueToNextLine = false;
        var isAddingRichText = false;

        yield return new WaitForSeconds(0.001f);

        foreach (var letter in line.ToCharArray())
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                SetText(line);
                break;
            }
            
            isAddingRichText = letter == '<' || isAddingRichText;

            if (letter == '>')
                isAddingRichText = false;

            Add(letter);

            if (isAddingRichText == false)
                yield return new WaitForSeconds(cooldownNewLetter);
        }

        CanCountinueToNextLine = true;
        IsStatusAnswer = dialogueChoice.DisplayChoices(story);
    }
}
