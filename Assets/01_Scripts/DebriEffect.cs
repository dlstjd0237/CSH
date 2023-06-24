using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebriEffect : PoolableMono
{
    private List<ParticleSystem> _particles = new List<ParticleSystem>();
    private void Awake()
    {
        //�̷��� �ϸ� �ڱ��ڽ�/�ڽĿ� �ִ� ��� ��ƼŬ �ý����� �����ͼ� _particles�� �־��ش�.
        
        GetComponentsInChildren<ParticleSystem>(_particles);
    }

    public void Explosion(Vector2 dir, float power)
    {
        foreach (ParticleSystem p in _particles)
        {
            var velocityModule = p.velocityOverLifetime;
            velocityModule.x = dir.x * power;
            velocityModule.y = dir.y * power;

            p.Play();
        }

        StartCoroutine(DelayCoroutine(2f));//2���� ��@��
    }

    IEnumerator DelayCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        PoolManager.Instance.Push(this);

    }
    public override void Init()
    {
        foreach (ParticleSystem p in _particles)
        {
            p.Simulate(0);
        }
    }
}
