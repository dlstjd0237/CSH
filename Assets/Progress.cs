using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Progress : MonoBehaviour
{
    private Image _progressBar;

    private void Awake()
    {
        _progressBar = GetComponent<Image>();
    }

    public void SetProgressBar(float Value,float MaxValue)
    {
        _progressBar.DOFillAmount(Value / MaxValue, 1);
    }
}
