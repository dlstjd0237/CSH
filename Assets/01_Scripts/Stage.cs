using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int BoxCount;
    public int CannonCount;

    private Transform _playerPosTrm;
    public Vector3 CannonPos => _playerPosTrm.position;

    private MapManager _mapManager;
    public MapManager MapManagerCompo => _mapManager;

    private void Awake()
    {
        
        _playerPosTrm = transform.Find("CannonPos");
        BoxCount = transform.Find("Boxes").childCount;
        _mapManager = GetComponent<MapManager>();
    }

}
