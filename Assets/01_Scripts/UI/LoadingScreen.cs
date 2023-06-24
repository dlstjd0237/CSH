using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    private RectTransform _rectTrm;
    private Image _loadingImage;

    private Vector2 _screenSize;
    private Coroutine _dotCo;
    private TextMeshProUGUI _textMesh;

    private void Awake()
    {
        _rectTrm = GetComponent<RectTransform>();
        _loadingImage = GetComponent<Image>();

        _textMesh = transform.Find("LoadingText").GetComponent<TextMeshProUGUI>();

        _screenSize = new Vector2(Screen.width, Screen.height);
        _rectTrm.sizeDelta = _screenSize; //최대 크기로 늘려준다

        _rectTrm.anchoredPosition = new Vector2(0, Screen.height);

        _textMesh.ForceMeshUpdate();
    }
    public void OpenLoadScreen()
    {
        StopAllCoroutines();
        Sequence seq = DOTween.Sequence();
        seq.Append(_rectTrm.DOAnchorPosY(0, 0.5f));
        seq.Join(_loadingImage.DOFade(1f, 0.5f));
        _dotCo = StartCoroutine(DotCorutine());
    }

    IEnumerator DotCorutine()
    {
        TMP_TextInfo info = _textMesh.textInfo;
        int cnt = info.characterCount;
        int dotCount = 3;
        

        while (true)
        {
            dotCount = (dotCount + 1) % 4;//0 1 2 3
            _textMesh.maxVisibleCharacters = cnt - dotCount; //끝에서 점 3개는 보이지 않게 됨
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void CloseLoadScreen()
    {
        StopCoroutine(_dotCo);
        Sequence seq = DOTween.Sequence();
        seq.Append(_rectTrm.DOAnchorPosY(_screenSize.y, 0.5f));
        seq.Join(_loadingImage.DOFade(0f, 0.5f));
    }


}
