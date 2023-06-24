using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class Explosion : PoolableMono
{
    private ParticleSystem _expParticle;
    private Light2D _light;
    private void Awake()
    {
        _expParticle = GetComponent<ParticleSystem>();
        _light = transform.Find("Light 2D").GetComponent<Light2D>();
        _light.enabled = false;
    }
    public void PlayExplosion()
    {
        _light.enabled = true;
        _expParticle.Play();
        StartCoroutine(DelayCoroutine(2f));
    }

    IEnumerator DelayCoroutine(float time)
    {
        //while (true)
        //{
        //    _light.intensity -= 0.1f;
        //    yield return new WaitForSeconds(0.01f); 존나 야메
        //    if(_light.intensity <= 0)
        //    {
        //        break;
        //    }
        //}

        float percent = 0;
        float currentTIme = 0; ;
        while(percent < 1)
        {
            currentTIme += Time.deltaTime;
            percent = currentTIme / time;
            _light.intensity = Mathf.Lerp(1.2f, 0, percent);
            yield return null;
        }

        PoolManager.Instance.Push(this);

    }
    public override void Init()
    {
        _light.enabled = false;
        _light.intensity = 1.2f;
        _expParticle.Simulate(0);//초기로 되감기
    }

}
