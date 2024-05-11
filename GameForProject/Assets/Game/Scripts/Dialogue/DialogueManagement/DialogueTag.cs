using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

[RequireComponent(typeof(Tags))]

public class DialogueTag : MonoBehaviour
{
    private Tags tags;

    public void Init()
    {
        tags = GetComponent<Tags>();
    }

    public void HandleTags(List<string> tagsList)
    {
        if(tagsList.Count == 0)
            return;
        
        foreach (var tagValue in tagsList)
        {
            var keyTag = tagValue.Split(':');

            if (keyTag.Length != 2)
                throw new ArgumentException("Неправильное оформление тега, просьба исправить");

            var key = keyTag [0].Trim();
            var value = keyTag[1].Trim();

            tags.GetValue(key).Calling(value);
        }
    }
}
