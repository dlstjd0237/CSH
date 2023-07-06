using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDate",menuName = "SO/EnemyDate", order =1)]

public class EnemyDate : ScriptableObject
{
    [Header("���� �̼� �ּҰ�")]
    [SerializeField] private float _enemyMinSpeed;

    [Header("���� �̼� �ִ밪")]
    [SerializeField] private float _enemyMaxSpeed;

    [Header("���� ���� �Ŀ�")]
    [Tooltip("5�� ������")]
    [SerializeField] private float _jumpPower;

    [Header("���� ���� ��")]
    [Tooltip("0.7~1�� ������")]
    [SerializeField] private float _jumpWai;

    [Header("���� �ִϸ��̼� ����?")]
    [SerializeField] private bool _onjump;

    public float EnemyMinSpeed { get { return _enemyMinSpeed; } }
    public float EnemyMaxSpeed { get { return _enemyMaxSpeed; } }
    public float JumpPower { get { return _jumpPower; } }
    public float JumpWai { get { return _jumpWai; } }
    public bool OnJump { get { return _onjump; } }
}
