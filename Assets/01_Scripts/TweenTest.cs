using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TheGreatGGM;
public class TweenTest : MonoBehaviour
{

    private Sequence _prevSeq = null;
    //Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    void Start()
    {

    }

    void Update()
    {
        float value = transform.GGM();
        //Debug.Log(value);
        CheckClick();
    }
    void CheckClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            if (_prevSeq != null&&_prevSeq.IsActive())
            {
                //_prevSeq.Complete();
                _prevSeq.Kill();
            }
            _prevSeq = DOTween.Sequence();
            

            var t1 = transform.DOMove(pos, 1f).SetEase(Ease.InCubic);//1초동안 움직임
            var t2 = transform.DORotate(new Vector3(0, 0, 360f), 1f, RotateMode.FastBeyond360);
            var t3 = transform.DOShakePosition(duration: 1, vibrato: 20, strength: 0.1f);//0.1초동안 흔들림
            _prevSeq.AppendInterval(1f);
            _prevSeq.Append(t1);
            _prevSeq.Join(t2);
            _prevSeq.AppendCallback(MakeParticle);
            _prevSeq.Append(t3);
            



            //StopAllCoroutines();
            //StartCoroutine(MoveCoroutine(pos, 1f));

        }
    }
    private void MakeParticle()
    {
        Debug.Log("파티클");
    }
    IEnumerator MoveCoroutine(Vector3 targetPos, float time)
    {
        Vector3 startPos = transform.position;
        float currentTime = 0f;
        float percent = 0f;
        while (percent < 1f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / time;
            float y = easeOutCubic(percent);

            Vector3 delta = targetPos - startPos;

            //transform.Translate(delta*)
            transform.position = startPos + delta * y;
            yield return null;
        }
        transform.position = targetPos;
    }
    private float easeOutCubic(float x)
    {
        return 1 - Mathf.Pow(1 - x, 3);
    }
    private float easeOutElastic(float x)
    {
        const float c4 = (2 * Mathf.PI) / 3;
        {


            return x == 0
              ? 0
              : x == 1
              ? 1
              : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1;
        }
    }
}

