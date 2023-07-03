using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class StartCam : MonoBehaviour
{
    private PlayableDirector _playable;
    UIToolkit _uiToolkit;
    bool qwer = false;
    private void Awake()
    {
        _uiToolkit = FindAnyObjectByType<UIToolkit>();
        _playable = FindAnyObjectByType<PlayableDirector>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&& qwer==false)
        {
            qwer = true;
            _playable.time = 4f;
            //_uiToolkit.StartUI();
        }
    }
}
