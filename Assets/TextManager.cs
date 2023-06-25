using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _startText;

    void Start()
    {
        StartCoroutine(AddAlpha());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator AddAlpha()
    {
        _startText.color = new Color(_startText.color.r, _startText.color.g, _startText.color.b, 0);
        while (_startText.color.a < 1.0f) 
        {
            _startText.color = new Color(_startText.color.r, _startText.color.g, _startText.color.b, _startText.color.a+(Time.deltaTime/2.0f));
            yield return null;
        }
        StartCoroutine(ReMoveoveAlpha());
    }
    IEnumerator ReMoveoveAlpha()
    {
        _startText.color = new Color(_startText.color.r, _startText.color.g, _startText.color.b, 1);
        while (_startText.color.a > 0.0f)
        {
            _startText.color = new Color(_startText.color.r, _startText.color.g, _startText.color.b, _startText.color.a-(Time.deltaTime/2.0f));
            yield return null;
        }
        StartCoroutine(AddAlpha());
    }
}

