using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDate",menuName = "SO/EnemyDate", order =1)]

public class EnemyDate : ScriptableObject
{
    [Header("몬스터 이속 최소값")]
    [SerializeField] private float _enemyMinSpeed;

    [Header("몬스터 이속 최대값")]
    [SerializeField] private float _enemyMaxSpeed;

    [Header("몬스터 점프 파워")]
    [Tooltip("5가 적당함")]
    [SerializeField] private float _jumpPower;

    [Header("몬스터 점프 쿨")]
    [Tooltip("0.7~1이 적당함")]
    [SerializeField] private float _jumpWai;

    [Header("점프 애니매이션 있음?")]
    [SerializeField] private bool _onjump;

    public float EnemyMinSpeed { get { return _enemyMinSpeed; } }
    public float EnemyMaxSpeed { get { return _enemyMaxSpeed; } }
    public float JumpPower { get { return _jumpPower; } }
    public float JumpWai { get { return _jumpWai; } }
    public bool OnJump { get { return _onjump; } }
}
