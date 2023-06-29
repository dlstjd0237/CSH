using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class InGameUIToolkitControll : MonoBehaviour
{
    private UIDocument _doc;

    private VisualElement _optionElement;

    private bool _onOption = false;
    private void Awake()
    {
        _doc = GetComponent<UIDocument>();

        _optionElement = _doc.rootVisualElement.Q<VisualElement>("Option");

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_onOption)
            {
                Time.timeScale = 1;
                _optionElement.AddToClassList("on");
                _onOption = false;
            }
            else
            {
                Time.timeScale = 0;
                _optionElement.RemoveFromClassList("on");
                _onOption = true;
            }
        }
    }
}
