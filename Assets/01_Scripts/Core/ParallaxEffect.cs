using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private Vector2 _parallaxRatio; //얘가 카메라를 몇퍼센트 반영할지 x,y 축으로

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

        //여기서 텍스쳐 유닛사이즈를 구해야 한다.
        Sprite s = GetComponent<SpriteRenderer>().sprite;
        Texture2D t = s.texture;

        _textureUnitSizeX = t.width / s.pixelsPerUnit;//이 텍스쳐의 너비가 몇 유닛인지 나오겠지?

        _isActive = true;
    }
    private void LateUpdate()//Updata 다음에 실행되는 거임
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
