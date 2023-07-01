using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private EnemyDate _enemyDate;
    private float _speed;
    private Transform _player;
    private Vector3 _dir;
    private RaycastHit2D rayHit;
    private Rigidbody2D _rig2D;
    private void Awake()
    {
        _speed = _enemyDate.EnemySpeed;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _dir = (_player.position - transform.position).normalized;
        _rig2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rayHit = Physics2D.Raycast(_rig2D.position, Vector2.left, 2.5f, LayerMask.GetMask("Ground"));
        if (rayHit.collider != null)
        {
            for (int i = 0; i < 3; i++)
            {

                _rig2D.AddForce(new Vector3(0, 0.4f, 0), ForceMode2D.Impulse);
            }
        }
        transform.position += _dir * _speed * Time.deltaTime;
    }
}
