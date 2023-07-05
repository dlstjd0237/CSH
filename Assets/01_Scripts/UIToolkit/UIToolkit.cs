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
    private VisualElement _helpMenu;
    [SerializeField]
    private UnityEngine.UI.Image _fadeImager;

    private Button _menuStartButton;
    private Button _menuSettingButton;
    private Button _menuExitButton;
    private Button _exitNoButton;
    private Button _exitYesButton;
    private Button _settingBackButton;
    private Button _settingHelpButton;
    private Button _helpBackButton;

    private Slider _backGroundMusic;
    private Slider _effectSound;

    private AudioSource _audiioSource;

    private BackGroundAudio _backAudio;

    private bool qwer = false;
    private void Awake()
    {
        _audiioSource = GetComponent<AudioSource>();


        _backAudio = FindAnyObjectByType<BackGroundAudio>();

        _doc = GetComponent<UIDocument>();
        _left = _doc.rootVisualElement.Q<VisualElement>("Left");
        _exitMenu = _doc.rootVisualElement.Q<VisualElement>("Exit");
        _settingMenu = _doc.rootVisualElement.Q<VisualElement>("Setting");
        _helpMenu = _doc.rootVisualElement.Q<VisualElement>("Helpmenu");

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

        _settingHelpButton = _doc.rootVisualElement.Q<Button>("HelpButton");
        _settingHelpButton.clicked += OnHelpMenu;

        _helpBackButton = _doc.rootVisualElement.Q<Button>("HelpBack");
        _helpBackButton.clicked += OffHelpMenu;

        _backGroundMusic = _doc.rootVisualElement.Q<Slider>("BackGroundMusic");
        _effectSound = _doc.rootVisualElement.Q<Slider>("EffectSound");
    }

    private void OffHelpMenu()
    {
        _audiioSource.Play();
        _helpMenu.AddToClassList("show");
        _settingMenu.RemoveFromClassList("on");
    }

    private void OnHelpMenu()
    {
        _audiioSource.Play();
        _settingMenu.AddToClassList("on");
        _helpMenu.RemoveFromClassList("show");
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
        _audiioSource.Play();
        _settingMenu.AddToClassList("on");
        _left.RemoveFromClassList("on");

    }

    private void SettingOn()
    {
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
        _audiioSource.Play();
        _exitMenu.AddToClassList("on");
        Application.Quit();
    }

    private void ExitNoButtonOn()
    {
        _audiioSource.Play();
        _exitMenu.AddToClassList("on");
        _left.RemoveFromClassList("on");
    }

    private void ExitmenuOn()
    {
        _audiioSource.Play();
        _left.AddToClassList("on");
        _exitMenu.RemoveFromClassList("on");

    }
    private void Start()
    {
        StartCoroutine(Co());
    }
    IEnumerator Co()
    {
        yield return new WaitForSeconds(6.6f);
        StartCam _startCam = FindAnyObjectByType<StartCam>();
        if(_startCam.qwer==false)
        _left.RemoveFromClassList("on");
        _startCam.qwer = true;
    }
    public void StartUI()
    {

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
