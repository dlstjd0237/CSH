using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private TextMeshProUGUI _msgText;
    private Transform _gaugeTrm;
    private TextMeshProUGUI _powrText;
    private Image _gaugeImage;
    private bool _checkSpace = false;
    private PlayerProfile _playerProfile;
    private float _camHalfWidth = 0;//카메라의 절반 너비
    private bool _isShowProfile = false;
    private Camera _mainCam;

    private LoadingScreen _loadingScreen;

    private TopInfoPanel _topInfoPanel;
    [SerializeField]
    private TMP_Text _lvelUpText;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        SetFillGayge(0,1);
    }
    public void LvelUpTextUpdate()
    {
        StartCoroutine(LvelUpTextUpdateCo());
    }
    public void Init(Transform canvasTrm)
    {
        _gaugeTrm = canvasTrm.Find("BottomBar/GaugeBar");
        _gaugeImage = canvasTrm.Find("BottomBar/GaugeBar/FillMask/Image").GetComponent<Image>();
        _powrText = _gaugeTrm.Find("GaugeText").GetComponent<TextMeshProUGUI>();
        _msgText = canvasTrm.Find("BottomBar/MsgText").GetComponent<TextMeshProUGUI>();
        _msgText.enabled = false;
        _lvelUpText = canvasTrm.Find("LvelUp").GetComponent<TMP_Text>();
        //텍스트 없으니 페스
        _loadingScreen = canvasTrm.Find("LoadingScreen").GetComponent<LoadingScreen>();
        GameManager.Instance.OnStageLoadStart += _loadingScreen.OpenLoadScreen;
        GameManager.Instance.OnStageLoasdComplete += _loadingScreen.CloseLoadScreen;

        _topInfoPanel = canvasTrm.Find("TopInfoPanel").GetComponent<TopInfoPanel>();
        GameManager.Instance.OnInfoChange += _topInfoPanel.SetText;// 이걸 구독하면 정보변경 이벤트가 올때마다

        _mainCam = Camera.main;
          _playerProfile = canvasTrm.Find("BottomBar/PlayerProfile").GetComponent<PlayerProfile>();
        _camHalfWidth = _mainCam.orthographicSize * _mainCam.aspect;
        _playerProfile.SetVisible(false);
    }
    public void ShowMsgText(string msg, Action Callback)
    {
        _msgText.enabled = true;
        _msgText.SetText(msg);

        _msgText.DOFade(endValue: 0.3f, duration: 0.5f).SetLoops(-1, LoopType.Yoyo);
        StartCoroutine(WaitSpaceBar(Callback));

    }

    private void Update()
    {
        if (_checkSpace == false && Input.GetButtonDown("Jump"))
        {
            _checkSpace = true;
        }
    }
    private void LateUpdate()
    {
        Vector3 playerPos = GameManager.Instance.PlayerTrm.position;

        if (Mathf.Abs(playerPos.x - _mainCam.transform.position.x) + 1f > _camHalfWidth
           && _isShowProfile == false)
        {
            _isShowProfile = true;
            _playerProfile.SetVisible(_isShowProfile);
        }
        else if (Mathf.Abs(playerPos.x - _mainCam.transform.position.x) + 1f < _camHalfWidth
            && _isShowProfile == true)
        {
            _isShowProfile = false;
            _playerProfile.SetVisible(_isShowProfile);
        }
    }

    IEnumerator WaitSpaceBar(Action Callback)
    {
        _checkSpace = false;

        yield return new WaitUntil(() => _checkSpace);

        _checkSpace = false;
        _msgText.DOKill();
        _msgText.alpha = 1;
        _msgText.enabled = false;
        Callback.Invoke();
    }

    public void SetFillGayge(float current, float max)
    {
        float ratio = current / max;
        _gaugeImage.fillAmount = ratio;
        _powrText.SetText(Mathf.CeilToInt(current).ToString());

        _powrText.fontSize = Mathf.Lerp(45f, 80f, ratio);
    }
    public IEnumerator LvelUpTextUpdateCo()
    {
        _lvelUpText.color = new Color(_lvelUpText.color.r, _lvelUpText.color.g, _lvelUpText.color.b, 1);
        yield return new WaitForSeconds(1);
        while (_lvelUpText.color.a > 0)
        {
            _lvelUpText.color = new Color(_lvelUpText.color.r, _lvelUpText.color.g, _lvelUpText.color.b, _lvelUpText.color.a - (Time.deltaTime / 2)) ;
            yield return null;
        }
    }
}


