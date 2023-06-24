using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private Vector2 _parallaxRatio; //�갡 ī�޶� ���ۼ�Ʈ �ݿ����� x,y ������

    private Transform _mainCamTrm;
    private Vector3 _lastCamPos;

    private float _textureUnitSizeX;

    private bool _isActive = false;
    private void Start()
    {
        GameManager.Instance.OnStageLoasdComplete += StartParallax;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnStageLoasdComplete -= StartParallax;
    }
    private void StartParallax()
    {
        _mainCamTrm = Camera.main.transform;
        _lastCamPos = _mainCamTrm.position;

        //���⼭ �ؽ��� ���ֻ���� ���ؾ� �Ѵ�.
        Sprite s = GetComponent<SpriteRenderer>().sprite;
        Texture2D t = s.texture;

        _textureUnitSizeX = t.width / s.pixelsPerUnit;//�� �ؽ����� �ʺ� �� �������� ��������?

        _isActive = true;
    }
    private void LateUpdate()//Updata ������ ����Ǵ� ����
    {
        if (_isActive == false) return;

        Vector3 deltMove = _mainCamTrm.position - _lastCamPos;
        transform.Translate(
            new Vector3(deltMove.x * _parallaxRatio.x, deltMove.y * _parallaxRatio.y),
            Space.World);
        _lastCamPos = _mainCamTrm.position;

        if(Mathf.Abs(_mainCamTrm.position.x - transform.position.x) >= _textureUnitSizeX)
        {
            float offsetX = (_mainCamTrm.position.x - transform.position.x) % _textureUnitSizeX;
            transform.position = new Vector3(_mainCamTrm.position.x - offsetX, transform.position.y);
        }
    }
}
