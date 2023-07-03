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

    [SerializeField] private LayerMask _whatIsBox; //비트로 이루어진 플래그들인데 몇번레이어가 거기 속하는지 보는 마스크다.
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
      
        UIManager.Instance.ShowMsgText("계속 하시려면 스페이스바를 누르세요", _onDestroyCallback);
        TimeController.Instance.SetTimeFreeze(freezeValue:0.2f,beforeDelay: 0.1f,freeaeTime: 0.2f);

        PoolManager.Instance.Push(this);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isActive == false) return;

        _isActive = false; //한번만 통지 받도록
        float expRadius = 5f;
        Explosion effect = PoolManager.Instance.Pop("ExplsionParticle") as Explosion;
        effect.transform.position = transform.position;
        effect.PlayExplosion();

        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, expRadius, _whatIsBox); //transform.position 은 중심점, 3f는 반지름 , _whatIsBox는 탐색할 마스크
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
