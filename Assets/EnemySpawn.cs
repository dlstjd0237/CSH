using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemy;
    private List<GameObject> _enemyList = new List<GameObject>();

    [SerializeField]
    [Header("Ǯ�� ����")]
    private int _poolCount;

    [SerializeField]
    [Header("���� ������")]
    private float _spawnCool;

    [SerializeField]
    [Header("������ ��ġ")]
    private Vector3 _spawnPos;

    private int _currentCount = 0;

    private void Awake()
    {
        for (int i = 0; i < _poolCount; i++)
        {
            GameObject qwer = Instantiate(_enemy[Random.Range(0, _enemy.Length)], transform);
            _enemyList.Add(qwer);
            qwer.SetActive(false);
        }
        StartCoroutine("Spawn");
    }
    private IEnumerator Spawn()
    {
        var _wai = new WaitForSeconds(_spawnCool);
        while (true)
        {
            _enemyList[_currentCount].SetActive(true);
            _enemyList[_currentCount].transform.position = _spawnPos;
            _currentCount++;
            if (_currentCount > _enemyList.Count - 1) _currentCount = 0;
            yield return _wai;
        }
    }
}
