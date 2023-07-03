using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDate",menuName = "SO/EnemyDate", order =1)]

public class EnemyDate : ScriptableObject
{
    [Header("���� �̼�")]
    [SerializeField] private float _enemySpeed;
    [Header("���� ���� �Ŀ�")]
    [Tooltip("5�� ������")]
    [SerializeField] private float _jumpPower;
    [Header("���� ���� ��")]
    [Tooltip("0.7~1�� ������")]
    [SerializeField] private float _jumpWai;

    public float EnemySpeed { get { return _enemySpeed; } }
    public float JumpPower { get { return _jumpPower; } }
    public float JumpWai { get { return _jumpWai; } }
}
