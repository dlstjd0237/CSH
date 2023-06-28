using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIToolkit : MonoBehaviour
{
    private UIDocument _doc;

    private VisualElement _left;
    private VisualElement _exitMenu;
    private VisualElement _settingMenu;

    [SerializeField]
    private UnityEngine.UI.Image _fadeImager;

    private Button _menuStartButton;
    private Button _menuSettingButton;
    private Button _menuExitButton;
    private Button _exitNoButton;
    private Button _exitYesButton;
    private Button _settingBackButton;

    private Slider _backGroundMusic;
    private Slider _effectSound;

    private AudioSource _audiioSource;

    private BackGroundAudio _backAudio;
    
    private void Awake()
    {
        _audiioSource = GetComponent<AudioSource>();



        _backAudio = FindAnyObjectByType<BackGroundAudio>();

        _doc = GetComponent<UIDocument>();
        _left = _doc.rootVisualElement.Q<VisualElement>("Left");
        _exitMenu = _doc.rootVisualElement.Q<VisualElement>("Exit");
        _settingMenu = _doc.rootVisualElement.Q<VisualElement>("Setting");

        _menuStartButton = _doc.rootVisualElement.Q<Button>("PlayButton");
        _menuStartButton.clicked += StartGame;

        _menuSettingButton = _doc.rootVisualElement.Q<Button>("SettingButton");
        _menuSettingButton.clicked += SettingOn;

        _menuExitButton = _doc.rootVisualElement.Q<Button>("ExitButton");
        _menuExitButton.clicked += ExitmenuOn;

        _exitNoButton = _doc.rootVisualElement.Q<Button>("ExitNoButton");
        _exitNoButton.clicked += ExitNoButtonOn;

        _exitYesButton = _doc.rootVisualElement.Q<Button>("ExitYetButton");
        _exitYesButton.clicked += ExitYesButtonOn;

        _settingBackButton = _doc.rootVisualElement.Q<Button>("Back");
        _settingBackButton.clicked += SettingBack;



        _backGroundMusic = _doc.rootVisualElement.Q<Slider>("BackGroundMusic");
        _effectSound = _doc.rootVisualElement.Q<Slider>("EffectSound");
    }

    private void Update()
    {
        _backAudio.SetBackGeound((float)_backGroundMusic.value / 100);
        EffectSoundSet();
    }
    private void EffectSoundSet()
    {
        PlayerPrefs.SetFloat("EffectSound", (float)_effectSound.value / 100);

        _audiioSource.volume = PlayerPrefs.GetFloat("EffectSound");
    }
    private void SettingBack()
    {
        //_audiioSource.clip = intro_music[1];
        _audiioSource.Play();
        _settingMenu.AddToClassList("on");
        _left.RemoveFromClassList("on");

    }

    private void SettingOn()
    {
        //_audiioSource.clip = intro_music[1];
        _audiioSource.Play();
        _left.AddToClassList("on");
        _settingMenu.RemoveFromClassList("on");
    }

    private void StartGame() // 게임 시작
    {
        StartCoroutine(FadeIn());
        _audiioSource.Play();
        _left.AddToClassList("on");
    }
    private void Loading()
    {
        LoadingSceneController.LoadScene("GameScene");
    }

    private void ExitYesButtonOn()
    {
        //_audiioSource.clip = intro_music[1];
        _audiioSource.Play();
        _exitMenu.AddToClassList("on");
        Debug.Log("게임 종료됨");
        Application.Quit();
    }

    private void ExitNoButtonOn()
    {
        //_audiioSource.clip = intro_music[1];
        _audiioSource.Play();
        _exitMenu.AddToClassList("on");
        _left.RemoveFromClassList("on");
    }

    private void ExitmenuOn()
    {
        //_audiioSource.clip = intro_music[1];
        _audiioSource.Play();
        _left.AddToClassList("on");
        _exitMenu.RemoveFromClassList("on");

    }

    private void Start()
    {
        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(6.6f);
        _left.RemoveFromClassList("on");
    }
    private IEnumerator FadeIn()
    {
        _fadeImager.color = new Color(_fadeImager.color.r, _fadeImager.color.g, _fadeImager.color.b, 0);
        while (_fadeImager.color.a < 1)
        {
            _fadeImager.color = new Color(_fadeImager.color.r, _fadeImager.color.g, _fadeImager.color.b, _fadeImager.color.a + (Time.deltaTime / 2));
            yield return null;
        }
        Loading();
    }

   

}
