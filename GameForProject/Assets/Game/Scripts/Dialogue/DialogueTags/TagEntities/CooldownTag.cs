using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownTag : MonoBehaviour, ITag
{
    public void Calling(string value)
    {
        var number = (float)Convert.ToDouble(value.Replace(".", ","));

        var dialogueWindow = GetComponent<DialogueWindow>();
        try
        {
            dialogueWindow.SetCoolDown(number);
        }
        catch(ArgumentException ex)
        {
            Debug.LogError(ex.Message);
        }
    }
}
