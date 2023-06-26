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
    private VisualElement _backGround;

    private Button _menuExitButton;
    private Button _menuStartButton;
    private Button _exitNoButton;
    private Button _exitYesButton;

    private AudioSource _audiioSource;
    private void Awake()
    {
        _audiioSource = GetComponent<AudioSource>();

        _doc = GetComponent<UIDocument>();
        _left = _doc.rootVisualElement.Q<VisualElement>("Left");
        _exitMenu = _doc.rootVisualElement.Q<VisualElement>("Exit");
        _backGround = _doc.rootVisualElement.Q<VisualElement>("BackGround");
        _backGround.style.backgroundColor = new Color(0,0,0,0);
        StartCoroutine(ColorInOut());

        _menuStartButton = _doc.rootVisualElement.Q<Button>("PlayButto");
        _menuStartButton.clicked += StartGame;

        _menuExitButton = _doc.rootVisualElement.Q<Button>("ExitButton");
        _menuExitButton.clicked += ExitmenuOn;

        _exitNoButton = _doc.rootVisualElement.Q<Button>("ExitNoButton");
        _exitNoButton.clicked += ExitNoButtonOn;

        _exitYesButton = _doc.rootVisualElement.Q<Button>("ExitYetButton");
        _exitYesButton.clicked += ExitYesButtonOn;
    }

    private void StartGame()
    {
        _audiioSource.Play();
        _left.AddToClassList("on");
        Invoke("Loading", 2);     
    }
    private void Loading()
    {
        LoadingSceneController.LoadScene("GameScene");
    }

    private void ExitYesButtonOn()
    {
        _audiioSource.Play();
        _exitMenu.AddToClassList("on");
        Debug.Log("∞‘¿” ¡æ∑·µ ");
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
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(6.6f);
        _left.RemoveFromClassList("on");
    }
    IEnumerator ColorInOut()
    {

    }

}
