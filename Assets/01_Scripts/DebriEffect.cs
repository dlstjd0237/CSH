using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebriEffect : PoolableMono
{
    private List<ParticleSystem> _particles = new List<ParticleSystem>();
    private void Awake()
    {
        //이렇게 하면 자기자신/자식에 있는 모든 파티클 시스템을 가져와서 _particles에 넣어준다.
        
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

        StartCoroutine(DelayCoroutine(2f));//2초후 박@살
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
