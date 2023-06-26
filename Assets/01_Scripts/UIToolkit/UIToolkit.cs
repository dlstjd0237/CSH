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


    private Button _menuExitButton;
    private Button _menuStartButton;
    private Button _exitNoButton;
    private Button _exitYesButton;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        _left = _doc.rootVisualElement.Q<VisualElement>("Left");
        _exitMenu = _doc.rootVisualElement.Q<VisualElement>("Exit");

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
        _left.AddToClassList("on");
        Invoke("Loading", 2);     
    }
    private void Loading()
    {
        LoadingSceneController.LoadScene("GameScene");
    }

    private void ExitYesButtonOn()
    {
        _exitMenu.AddToClassList("on");
        Debug.Log("∞‘¿” ¡æ∑·µ ");
        Application.Quit();
    }

    private void ExitNoButtonOn()
    {
        _exitMenu.AddToClassList("on");
        _left.RemoveFromClassList("on");
    }

    private void ExitmenuOn()
    {
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

}
