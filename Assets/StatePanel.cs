using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StatePanel : MonoBehaviour
{
    private RectTransform _rectTrm;
    private Image _stateImage;

    private Vector2 _screenSize;

    private bool _state = false;
    private void Awake()
    {
        _rectTrm = GetComponent<RectTransform>();
        _stateImage = GetComponent<Image>();

        _screenSize = new Vector2(1500, 800);
        _rectTrm.sizeDelta = new Vector2(1050, 800);
        _rectTrm.anchoredPosition = new Vector2(Screen.width, -110);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!_state)
                OnScreen();
            else if (_state)
                OffScreen();

        }
    }

    private void OnScreen()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_rectTrm.DOAnchorPosX(-410, 0.5f));
        seq.Join(_stateImage.DOFade(1, 0.5f));
        _state = true;
    }

    private void OffScreen()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_rectTrm.DOAnchorPosX(_screenSize.x, 0.5f));
        seq.Join(_stateImage.DOFade(0, 0.5f));
        _state = false;
    }


}
