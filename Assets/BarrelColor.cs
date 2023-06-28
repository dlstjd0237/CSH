using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelColor : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _changeColor();
    }

    private void _changeColor()
    {
        switch (StateManager.Instance.CurrentClass)
        {
            case 0:
                _spriteRenderer.color = new Color(1, 0.4674684f, 0);
                break;
            case 1:
                _spriteRenderer.color = new Color(1, 1, 1);
                break;
            case 2:
                _spriteRenderer.color = new Color(1, 0.8510299f, 0);
                break;
            case 3:
                _spriteRenderer.color = new Color(0, 1, 0.8514516f);
                break;
            case 4:
                _spriteRenderer.color = new Color(0, 0.8458567f, 1);
                break;
            case 5:
                _spriteRenderer.color = new Color(0, 1, 0.1688542f);
                break;
        }
    }
}
