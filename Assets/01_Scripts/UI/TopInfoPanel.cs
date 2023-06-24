using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InfoCategory
{
    Cannon = 1,
    Crate = 2
}
//이 게임을 개조해서 재미나게 만들어오면
//1둥 : 3만원
//2등 : 1만5천원
//3등 : 5천원
//7월7일까지
public class TopInfoPanel : MonoBehaviour
{
    private Dictionary<InfoCategory, InfoBox> _infoDictionary;

    private void Awake()
    {
        _infoDictionary = new Dictionary<InfoCategory, InfoBox>();
        foreach (InfoCategory cat in Enum.GetValues(typeof(InfoCategory)))
        {
            InfoBox box = transform.Find($"{cat.ToString()}InfoBox").GetComponent<InfoBox>();
            _infoDictionary.Add(cat, box);
        }
    }

    public void SetText(InfoCategory cat, string text)
    {
        if(_infoDictionary.TryGetValue(cat,out var info))
        {
            info.SetText(text);
        }
    }
}
