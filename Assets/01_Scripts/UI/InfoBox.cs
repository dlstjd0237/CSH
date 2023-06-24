using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoBox : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;

    private void Awake()
    {
        _textMesh = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        _textMesh.SetText(text);
    }
}
