using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System;

public class DialogueChoice : MonoBehaviour
{
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    public void Init()
    {
        choicesText = new TextMeshProUGUI[choices.Length];

        ushort index = 0;
        foreach (var choise in choices)
            choicesText[index++] = choise.GetComponentInChildren<TextMeshProUGUI>();
    }

    public bool DisplayChoices(Story story)
    {
        var currentChoices = story.currentChoices.ToArray();

        if (currentChoices.Length > choices.Length)
            throw new ArgumentNullException("Ошибка! Выборов в сценарии больше, чем возможностей в игре!");

        HideChoices();

        ushort index = 0;

        foreach (var choice in currentChoices)
        {
            choices[index].SetActive(true);
            choicesText[index++].text = choice.text;
        }

        return currentChoices.Length > 0;
    }

    public void HideChoices()
    {
        foreach (var button in choices)
            button.SetActive(false);
    }
}
