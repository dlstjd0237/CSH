using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    private int _currentClass = 0; public int CurrentClass { get => _currentClass; set => Mathf.Max(0, value); }
    private float _currentMaxHp = 100; public float CurrentMaxHp { get => _currentMaxHp; set => Mathf.Max(0, value); }
    private float _currentHp = 0; public float CurrentHp { get => _currentHp; set => Mathf.Max(0, value); }
    private int _currentCoin = 0; public int CurrentCoin { get => _currentCoin; set => Mathf.Max(0, value); }
    private float _currentTime = 0; public float CurrentTime { get => _currentTime; set => Mathf.Max(0, value); }

    [SerializeField]
    private List<string> _class = new List<string>();

    [SerializeField]
    private TMP_Text[] _stateText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _stateText[0].text = "등급 : " + _class[_currentClass];
    }
    private void Update()
    {
        ChangeCurrentTime();
        ChangeCurrentHp();
        ChangeCurrentCoin();
        ChangerClass();
    }

    private void ChangerClass()
    {
        _stateText[0].text = "등급 : " + _class[_currentClass];
    }

    private void ChangeCurrentCoin()
    {
        _stateText[3].text = ""+_currentCoin;
    }

    private void ChangeCurrentHp()
    {
        _stateText[1].text = "성 체력 : " + _currentHp +"/"+ _currentMaxHp;
    }

    private void ChangeCurrentTime()
    {
        _currentTime += Time.deltaTime;
        _stateText[2].text = "플레이 타임 : " + (int)_currentTime;
    }

    public void UpClass()
    {
        _currentClass++;
    }
}
