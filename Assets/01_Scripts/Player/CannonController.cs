using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CannonState : short
{
    IDLE = 0,
    MOVING = 1,
    CHARGING = 2,
    FIRE = 3

}
public class CannonController : MonoBehaviour
{
    private Transform _barrelTrm, _firePosTrm; // ���⵵ �߰�����.
    private float _currentRotate = 0;
    private float _currentPower = 0;
    [SerializeField] float _rotateSpeedd = 180f, _maxFirePower = 800f, _chargingSpeed = 300f;
    [SerializeField] private CannonState _currentState = CannonState.IDLE; //���� idle ���·� ����
    [SerializeField] private CannonBall _ballPrefab; //�̰� �߰�����
    bool isgameover = false;
    private Transform _camRig;
    private Projectary _projectary;
    [SerializeField] float _projectaryTime = 2f;
    [SerializeField] int _projectaryCount = 20;
    [SerializeField] float ballMass = 1.4f;
    private void Awake()
    {
        _camRig = transform.Find("CameraRig");
        _barrelTrm = transform.Find("Barrel");
        _firePosTrm = transform.Find("Barrel/FirePos");
        _projectary = transform.Find("DrawProjectaries").GetComponent<Projectary>();
        _projectary.SetData(time: _projectaryTime, count: _projectaryCount);
    }
    void Start()
    {
        CameraManneger.Instance.ChangeActiveCam(CamerCategory.RigCam);
    }

    void Update()
    {
        if (!isgameover)
        {

            HandleMove();
            HandleFire();
            if (_currentState == CannonState.IDLE)
            {
                HandleView();
            }
        }

    }

    void HandleView()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float _speed = 20f;

        _camRig.Translate(new Vector3(x * _speed * Time.deltaTime, 0, 0), Space.World);
    }

    private void HandleFire()
    {
        if (Input.GetButtonDown("Jump") && (short)_currentState < 2)
        {
            _currentState = CannonState.CHARGING;
            _currentPower = 0;
        }
        if (Input.GetButton("Jump") && _currentState == CannonState.CHARGING)
        {
            _currentPower += _chargingSpeed * Time.deltaTime;
            _currentPower = Math.Clamp(_currentPower, 0, _maxFirePower);

            _projectary.DrawLine(_firePosTrm.position, _firePosTrm.right * _currentPower*(1/ballMass));

            UIManager.Instance.SetFillGayge(_currentPower, _maxFirePower);
        }

        if (Input.GetButtonUp("Jump") && _currentState == CannonState.CHARGING)
        {

            ReadyToFire();
        }
    }

    private void ReadyToFire()
    {
        _currentState = CannonState.FIRE;
        //�߻� �غ� �Ǹ� �����ī�޶� �ű�� ī�޶� �� �Ű����� �߻縦 �����Ѵ�.
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            CameraManneger.Instance.ChangeActiveCam(CamerCategory.CannonCam);//ĳ������ �Ű��ְ�
            CameraManneger.Instance.ChangeFollowTarget(CamerCategory.BallCam, transform);//�߻�� ī�޶� �̻��Ϸ� 
            _camRig.transform.position = transform.position;
        });
        seq.AppendInterval(1f);

        seq.AppendCallback(() =>
        {
            FireCannon();//���� �߻�
        });
    }

    private void FireCannon()
    {
        CannonBall ball = PoolManager.Instance.Pop("cannonBall") as CannonBall;
        ball.transform.position = _firePosTrm.position;
        ball.Fier(_firePosTrm.right * _currentPower, GoToIdle);

        CameraManneger.Instance.ChangeFollowTarget(CamerCategory.BallCam,ball.transform);//�߻�� ī�޶� �̻��Ϸ� 
        CameraManneger.Instance.ChangeActiveCam(CamerCategory.BallCam);

    }
    private void GoToIdle()
    {

        _currentState = CannonState.IDLE;
        //UIManager.Instance.ShowMsgText("��� �Ͻ÷��� �����̽��ٸ� ��������");
        CameraManneger.Instance.ChangeActiveCam(CamerCategory.RigCam);//������ ī�޶� ����ġ
    }

    private void HandleMove()
    {
        float y = Input.GetAxisRaw("Vertical"); //���� �Է¹ޱ�


        //float y_rotaton = y * _rotateSpeedd * Time.deltaTime;
        _currentRotate += y * Time.deltaTime * _rotateSpeedd;
        _currentRotate = Mathf.Clamp(_currentRotate, 0, 90f);

        _barrelTrm.rotation = Quaternion.Euler(0, 0, _currentRotate);//���Ϸ� 1�г⶧�� �̰͸� 

        //Quaternion.AngleAxis(30f, Vector3.forward); 
        //Quaternion.LookRotation(Vector3.forward);


        //��� ȸ���� -������ 90�� ������ ���� ȸ���� �� �ִ�. ȸ���� ������ �翬�O���̾�.
    }
}
