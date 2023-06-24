using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxDrooper : MonoBehaviour
{
    [SerializeField]
    private DebriEffect _effectPrefab;
    [SerializeField]
    private float _delayTime = 1f;
    private Camera _mainCam;
    private float _camHeight;
    private Vector3 _targetPos;

    private void Start()
    {
        
        //���⼭ �ڽ��� ȭ������� �����ٳ��� 
        _mainCam = Camera.main;
        _camHeight = _mainCam.orthographicSize * 2;
        _targetPos = transform.position; //���� ��ġ ����

        transform.Translate(new Vector3(0, _camHeight, 0));
        float time = 1f + Random.Range(-0.3f, 0.3f);
        PlayTween(time: time, delay: _delayTime);
    }

    public void PlayTween(float time, float delay)
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(delay);
        seq.Append(transform.DOMove(_targetPos, time).SetEase(Ease.InCubic));
        seq.Append(transform.DOShakePosition(duration: 0.5f, vibrato: 20, strength: 0.1f));
        seq.AppendCallback(() =>
        {
            DebriEffect effect = Instantiate(_effectPrefab, transform.position, Quaternion.identity);
            effect.Explosion(Vector2.up, 5f);

            Destroy(gameObject);
        });
        
    }
}
