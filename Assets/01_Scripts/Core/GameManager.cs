using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [SerializeField]
    private PoolingListSo _poolListSO;

    private Progress _progress;

    public event Action OnStageLoasdComplete = null;
    public event Action OnStageLoadStart = null;
    public event Action<InfoCategory, string> OnInfoChange = null;

    [SerializeField] CannonController _playerCannon;
    public Transform PlayerTrm => _playerCannon.transform;

    public static MapManager MapManagerInstance = null;
    #region 게임정보
    private int _currentcannonCnt = 0;
    private int _currentBoxCnt = 0;
    private int _stagePoint = 0;
    private int _maxStagePoint = 100;
    #endregion
    private int _stagenum = 1;
    public int expRadius = 1; //폭팔 범위

    #region 플레이어 파워 잠금
    private bool _lvel1 = false; public bool Lvel1 { get => _lvel1; set => _lvel1 = value; }
    private bool _lvel2 = false; public bool Lvel2 { get => _lvel2; set => _lvel2 = value; }
    private bool _lvel3 = false; public bool Lvel3 { get => _lvel3; set => _lvel3 = value; }
    private bool _lvel4 = false; public bool Lvel4 { get => _lvel4; set => _lvel4 = value; }
    private bool _lvel5 = false; public bool Lvel5 { get => _lvel5; set => _lvel5 = value; }
    #endregion

    private void Start()
    {
        LoadStage(_stagenum);
    }
    private void Awake()
    {


        //if (Instance != null)
        //{
        Instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        _progress = FindAnyObjectByType<Progress>();
        CreateUIManager();
        CreateCameraManager();

        CreateTimeController();
        CreatePoolManager();
    }


    private GameObject _currentLevel = null;
    private AsyncOperationHandle<GameObject> _stageLoadHandle;
    private void LoadStage(int number)
    {
        OnStageLoadStart?.Invoke();//스테이지 로딩이 시작된다.
        MapManagerInstance = null;
        Addressables.LoadAssetAsync<GameObject>($"Level{number.ToString()}").Completed += //로드가 다되면 액션을 실행하라
            (AsyncOperationHandle<GameObject> handle) =>
            {
                if (_currentLevel != null)
                {
                    Destroy(_currentLevel);
                    Addressables.Release(_stageLoadHandle);
                }
                _stageLoadHandle = handle;
                _currentLevel = Instantiate(handle.Result, Vector3.zero, Quaternion.identity);
                StartCoroutine(DelayStart()); //1초기 기다렸다가 시작 메세지 날리기
            };
    }
    bool qwer = false;
    public void AddStagePoint(int num)
    {
        _stagePoint += num;
        Debug.Log(_stagePoint);
        if (_stagePoint >= _maxStagePoint && qwer == false)
        {
            qwer = true;
            _stagenum++;
            LoadStage(_stagenum);
            _stagePoint = 0;
            _maxStagePoint += 50;
            qwer = false;
        }
        _progress.SetProgressBar(_stagePoint, _maxStagePoint);
    }
    private IEnumerator DelayStart()
    {
        Stage s = _currentLevel.GetComponent<Stage>();

        PlayerTrm.position = s.CannonPos;//처음 시작했을 때 맵상의 시작 위치로 이동해라.
        MapManagerInstance = s.MapManagerCompo;

        _currentBoxCnt = s.BoxCount;
        _currentcannonCnt = s.CannonCount;

        UpdateInfoPanel();
        yield return new WaitForSeconds(1f);
        OnStageLoasdComplete?.Invoke();
    }

    private void UpdateInfoPanel()
    {
        OnInfoChange?.Invoke(InfoCategory.Cannon, _currentcannonCnt.ToString());
        OnInfoChange?.Invoke(InfoCategory.Crate, _currentBoxCnt.ToString());

    }

    public void DecreaseBallAndCannon(int cannonCnt, int boxCnt)
    {
        _currentcannonCnt -= cannonCnt;
        _currentBoxCnt -= boxCnt;

        UpdateInfoPanel();

        if (_currentcannonCnt <= 0/*||_currentBoxCnt <= 0*/)
        {
            //SetGameOver();
        }
        if (_currentBoxCnt == 0)
        {
            _stagenum++;
            LoadStage(_stagenum);
        }
    }

    private void SetGameOver()
    {
        Debug.Log("게임 Over");
    }

    private void CreatePoolManager()
    {
        PoolManager.Instance = new PoolManager(transform);
        //여기서 필요한 애들 풀 만들어줘야 함
        foreach (PoolingPair pair in _poolListSO.Pairs)
        {
            PoolManager.Instance.CreatePool(pair.Prefab, pair.Count);
        }
    }

    private void CreateTimeController()
    {
        TimeController.Instance = gameObject.AddComponent<TimeController>();
    }

    private void CreateUIManager()
    {
        GameObject obj = new GameObject();
        UIManager.Instance = obj.AddComponent<UIManager>();

        obj.transform.parent = transform;

        Transform canvasTrm = FindAnyObjectByType<Canvas>().transform;
        UIManager.Instance.Init(canvasTrm);

    }

    private void CreateCameraManager()
    {
        CameraManneger.Instance = GameObject.Find("CameraSet").AddComponent<CameraManneger>();
        CameraManneger.Instance.Init();
    }
    public void SetLvel(int Lvel)
    {
        switch (Lvel)
        {
            case 1: if (!_lvel1) { _lvel1 = true; UIManager.Instance.LvelUpTextUpdate(); } break;
            case 2: if (!_lvel2) { _lvel2 = true; UIManager.Instance.LvelUpTextUpdate(); } break;
            case 3: if (!_lvel3) { _lvel3 = true; UIManager.Instance.LvelUpTextUpdate(); } break;
            case 4: if (!_lvel4) { _lvel4 = true; UIManager.Instance.LvelUpTextUpdate(); } break;
            case 5: if (!_lvel5) { _lvel5 = true; UIManager.Instance.LvelUpTextUpdate(); } break;
        }
    }
}
