using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpeakerTag), typeof(MethodTag), typeof(CooldownTag))]
public class Tags : MonoBehaviour
{
    private readonly Dictionary<string, ITag> map = new();

    private void Start() 
    {
        map.Add("speaker", GetComponent<SpeakerTag>());
        map.Add("method", GetComponent<MethodTag>());
        map.Add("cooldown", GetComponent<CooldownTag>());
    }

    public ITag GetValue(string key)
    {
        return map.GetValueOrDefault(key);
    }
}
