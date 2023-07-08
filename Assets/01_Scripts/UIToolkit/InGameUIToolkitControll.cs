using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class InGameUIToolkitControll : MonoBehaviour
{
    private AudioSource _audio;
    private BackGroundAudio _backGroundAudio;

    private UIDocument _doc;

    private VisualElement _optionElement;
    private VisualElement _buttonsElement;
    private VisualElement _settingElement;

    private Button _contunueButton; //계속하기 버튼
    private Button _settingButton;
    private Button _mainMenuButton;

    private Slider _backGroundSoundSlider; //배경음
    private Slider _effectSlider; //효과음

    private bool _onOption = false;
    private void Awake()
    {
        _backGroundAudio = FindAnyObjectByType<BackGroundAudio>();
        _audio = GetComponent<AudioSource>();

        _doc = GetComponent<UIDocument>();

        _optionElement = _doc.rootVisualElement.Q<VisualElement>("Option");
        _buttonsElement = _doc.rootVisualElement.Q<VisualElement>("Buttons");
        _settingElement = _doc.rootVisualElement.Q<VisualElement>("SettingEle");

        _contunueButton = _doc.rootVisualElement.Q<Button>("ContinueButton");
        _contunueButton.clicked += OptionOut;

        _settingButton = _doc.rootVisualElement.Q<Button>("SettingButton");
        _settingButton.clicked += OnSetting;

        _mainMenuButton = _doc.rootVisualElement.Q<Button>("GoMenu");
        _mainMenuButton.clicked += GoMenu;

        _backGroundSoundSlider = _doc.rootVisualElement.Q<Slider>("BackSlider");
        _backGroundSoundSlider.value = PlayerPrefs.GetFloat("BackGroundAudio") *100;
        _effectSlider = _doc.rootVisualElement.Q<Slider>("EffectSlider");
        _audio.volume = PlayerPrefs.GetFloat("EffectSound");
        _effectSlider.value = (float)_audio.volume * 100;

    }

    private void GoMenu()
    {
        OptionOut();
        LoadingSceneController.LoadScene("StartScene");
    }

    private void OnSetting()
    {
        _buttonsElement.AddToClassList("Move");
        _settingElement.RemoveFromClassList("OnSetting");
    }
    private void OffSetting()
    {
        _buttonsElement.RemoveFromClassList("Move");
        _settingElement.AddToClassList("OnSetting");
    }

    void Update()
    {
        _backGroundAudio.SetBackGeound((float)_backGroundSoundSlider.value / 100f);
        EffectSoundSet();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_onOption)
            {
                OptionOut();
 
                    
            }
            else
            {
                OptionIn();
            }
        }
                
    }
    private void EffectSoundSet()
    {       
        PlayerPrefs.SetFloat("EffectSound", (_effectSlider.value / 100));
        _audio.volume = PlayerPrefs.GetFloat("EffectSound");
    }
    private void OptionIn()
    {
        Time.timeScale = 0;
        _optionElement.RemoveFromClassList("on");
        _onOption = true;
    }
    private void OptionOut()
    {
        OffSetting();
        Time.timeScale = 1;
        _optionElement.AddToClassList("on");
        _onOption = false;
    }
}
