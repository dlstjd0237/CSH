using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDate",menuName = "SO/EnemyDate", order =1)]

public class EnemyDate : ScriptableObject
{
    [Header("몬스터 이속")]
    [SerializeField] private float _enemySpeed;

    public float EnemySpeed { get { return _enemySpeed; } }
}
