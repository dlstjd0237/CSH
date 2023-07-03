using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDate",menuName = "SO/EnemyDate", order =1)]

public class EnemyDate : ScriptableObject
{
    [Header("몬스터 이속")]
    [SerializeField] private float _enemySpeed;
    [Header("몬스터 점프 파워")]
    [Tooltip("5가 적당함")]
    [SerializeField] private float _jumpPower;
    [Header("몬스터 점프 쿨")]
    [Tooltip("0.7~1이 적당함")]
    [SerializeField] private float _jumpWai;

    public float EnemySpeed { get { return _enemySpeed; } }
    public float JumpPower { get { return _jumpPower; } }
    public float JumpWai { get { return _jumpWai; } }
}
