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

    [SerializeField]
    private TMP_Text _coinText;

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

        ChangeClass();
        ChangePointTest();
        ChangeCurrentCoin();
    }

    private void ChangePointTest()
    {
        switch (_currentClass)
        {
            case 0:
                _stateText[3].text = "Ư¡ : ������ ������ ��,\nȥ�� ���� ���Ѿ� ��.";
                break;
            case 1:
                _stateText[3].text = "Ư¡ : ������ ����� ��,\n����� ������.";
                break;
            case 2:
                _stateText[3].text = "Ư¡ : ������ ����� ��,\n�ȸ� ��� �� ����.";
                break;
            case 3:
                _stateText[3].text = "Ư¡ : ������ ��ü �÷�,\ndlstjd0237ģ�ߤ�";
                break;
            case 4:
                _stateText[3].text = "Ư¡ : ������ ���̾Ƹ�,\n�ѱ����.";
                break;
            case 5:
                _stateText[3].text = "Ư¡ : �ְ� ���,\n����� �� �μ���.";
                break;

        }

    }


    public void AddCoin(int _coin)
    {
        _currentCoin += _coin;
        ChangeCurrentCoin();
    }



    private void Update()
    {
        ChangeCurrentTime();
        ChangeCurrentHp();
    }

    private void ChangeClass()
    {
        _stateText[0].text = "��� : " + _class[_currentClass];

    }

    private void ChangeCurrentCoin()
    {
        _coinText.text = "" + _currentCoin;
    }

    private void ChangeCurrentHp()
    {
        _stateText[1].text = "�� ü�� : " + _currentHp + "/" + _currentMaxHp;
    }

    private void ChangeCurrentTime()
    {
        _currentTime += Time.deltaTime;
        _stateText[2].text = "�÷��� Ÿ�� : " + (int)_currentTime;
    }

    public void UpClass()
    {
        _currentClass++;
        ChangeClass();
        CahangClassColor();
        ChangePointTest();
    }

    private void CahangClassColor()
    {
        for (int i = 0; i < _stateText.Length; i++)
        {

            switch (_currentClass)
            {
                case 0:
                    _stateText[i].color = new Color(1, 0.4674684f, 0);
                    break;
                case 1:
                    _stateText[i].color = new Color(1, 1, 1);
                    break;
                case 2:
                    _stateText[i].color = new Color(1, 0.8510299f, 0);
                    break;
                case 3:
                    _stateText[i].color = new Color(0, 1, 0.8514516f);
                    break;
                case 4:
                    _stateText[i].color = new Color(0, 0.8458567f, 1);
                    break;
                case 5:
                    _stateText[i].color = new Color(0, 1, 0.1688542f);
                    break;
            }
        }
    }
}

