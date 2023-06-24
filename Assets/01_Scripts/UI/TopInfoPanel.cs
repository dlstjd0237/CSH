using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InfoCategory
{
    Cannon = 1,
    Crate = 2
}
//�� ������ �����ؼ� ��̳��� ��������
//1�� : 3����
//2�� : 1��5õ��
//3�� : 5õ��
//7��7�ϱ���
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
