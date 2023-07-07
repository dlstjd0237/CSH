using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private EnemyDate _enemyDate;
    #region EnemyState
    private float _speed;
    private float _jumpPower;
    private float _wai;
    #endregion
    private Transform _player;
    private Vector3 _dir;
    private RaycastHit2D rayHit;
    private Rigidbody2D _rig2D;
    private bool _isjump = false;
    private bool _onjump;
    private void Awake()
    {
        _onjump = _enemyDate.OnJump;
        _speed = Random.Range(_enemyDate.EnemyMinSpeed, _enemyDate.EnemyMaxSpeed);
        _jumpPower = _enemyDate.JumpPower;
        _wai = _enemyDate.JumpWai;


        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _dir = (_player.position - transform.position).normalized;
        _rig2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rayHit = Physics2D.Raycast(_rig2D.position, Vector2.left, 2.5f, LayerMask.GetMask("Ground", "Wall"));
        if (rayHit.collider != null && _isjump == false)
        {
            StartCoroutine(Jump());

        }
        transform.position += _dir * _speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && _isjump == false)
        {
            StartCoroutine(Jump());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DieBox"))
        {
            StateManager.Instance.SetHp(10f);
            gameObject.SetActive(false);
        }
    }
    private IEnumerator Jump()
    {
        _isjump = true;
        var wai = new WaitForSeconds(_wai);
        if (_onjump)
            _animator.SetBool("isjump", true);

        _rig2D.AddForce(new Vector3(0, _jumpPower, 0), ForceMode2D.Impulse);
        yield return wai;

        if (_onjump)
            _animator.SetBool("isjump", false);
        _isjump = false;
    }
}
