using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerProfile : MonoBehaviour
{
    private RectTransform _rectTrm;
    private Vector2 _intitAnchorPos;
    private void Awake()
    {
        _rectTrm = GetComponent<RectTransform>();
        _intitAnchorPos = _rectTrm.anchoredPosition;
    }
    public void SetVisible(bool value)
    {

        _rectTrm.DOKill();
        if (value)
        {
            _rectTrm.DOAnchorPos(_intitAnchorPos, 0.6f);
        }
        else
        {
            float moveX = _rectTrm.sizeDelta.x + 10f;
            Vector2 target = new Vector2(moveX, _intitAnchorPos.y);
            _rectTrm.DOAnchorPos(target,0.6f);
        }
    }

    #region 디버그용 코드
    private void Update()
    {
        //new InputSystem
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetVisible(true);
        }
        else if( Input.GetKeyDown(KeyCode.O))
        {
            SetVisible(false);
        }
    }
    #endregion
}
