using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CannonBall : PoolableMono
{
    Rigidbody2D Rig2D;

    Action _onDestroyCallback;

    [SerializeField] float _lifeTime = 3.0f;
    private float _currentLifeTime = 0.0f;

    [SerializeField] private LayerMask _whatIsBox; //��Ʈ�� �̷���� �÷��׵��ε� ������̾ �ű� ���ϴ��� ���� ����ũ��.
    [SerializeField] private Explosion _expEffect;

    private bool _isActive = true;
    // Start is called before the first frame update
    private void Awake()
    {
        Rig2D = GetComponent<Rigidbody2D>();
       
    }
    public void Fier(Vector2 dir, Action Callback)
    {
        Rig2D.AddForce(dir, ForceMode2D.Impulse);
        _onDestroyCallback = Callback;
        StartCoroutine(DelayDead());
    }

    IEnumerator DelayDead()
    {
        yield return new WaitForSeconds(_lifeTime);
        DestroyCannonBall();
    }

    private void DestroyCannonBall()
    {
      
        UIManager.Instance.ShowMsgText("��� �Ͻ÷��� �����̽��ٸ� ��������", _onDestroyCallback);
        TimeController.Instance.SetTimeFreeze(freezeValue:0.2f,beforeDelay: 0.1f,freeaeTime: 0.2f);

        PoolManager.Instance.Push(this);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isActive == false) return;

        _isActive = false; //�ѹ��� ���� �޵���
        float expRadius = 5f;
        Explosion effect = PoolManager.Instance.Pop("ExplsionParticle") as Explosion;
        effect.transform.position = transform.position;
        effect.PlayExplosion();

        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, expRadius, _whatIsBox); //transform.position �� �߽���, 3f�� ������ , _whatIsBox�� Ž���� ����ũ
        foreach(Collider2D col in colls)
        {
            if(col.TryGetComponent<Box>(out Box box))
            {
                Vector2 dir = col.transform.position - transform.position;
                float power = Mathf.Lerp(7f,3f, dir.magnitude / 4f);
                box.DestroyBox(dir.normalized, power);
            }
        }
        CameraManneger.Instance.ShakeCam(0.8f, 3f);

        GameManager.MapManagerInstance.CheckDestroy(transform.position, expRadius);

        GameManager.Instance.DecreaseBallAndCannon(cannonCnt: 1, boxCnt: colls.Length);
        DestroyCannonBall();
    }


    private void Update()
    {
        _currentLifeTime += Time.deltaTime;
        if (_currentLifeTime >= _lifeTime)
        {
            GameManager.Instance.DecreaseBallAndCannon(cannonCnt: 1, boxCnt: 0);
            DestroyCannonBall();
        }
    }
    public override void Init()
    {
        _currentLifeTime = 0;
        _isActive = true;
    }
}
