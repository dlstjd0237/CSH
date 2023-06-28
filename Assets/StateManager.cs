using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    private int _currentLv = 1;   public int CurrentLv { get => _currentLv; set => Mathf.Max(0, value); }
    private float _currentMaxExp = 1000;   public float CurrentMaxExp { get => _currentMaxExp; set => Mathf.Max(0, value); }
    private float _currentExp = 0;  public float CurrentExp { get => _currentExp; set => Mathf.Max(0, value); }
    private int _currentCoin = 0; public int CurrentCoin { get => _currentCoin; set => Mathf.Max(0, value); }
   

}
