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

    public event Action OnStageLoasdComplete = null;
    public event Action OnStageLoadStart = null;
    public event Action<InfoCategory, string> OnInfoChange = null;

    [SerializeField] CannonController _playerCannon;
    public Transform PlayerTrm => _playerCannon.transform;

    public static MapManager MapManagerInstance = null;
    #region ��������
    private int _currentcannonCnt = 0;
    private int _currentBoxCnt = 0;
    #endregion
    private int _stagenum = 1;

    private void Start()
    {
        LoadStage(_stagenum);
    }
    private void Awake()
    {   
       

        //if(Instance != null)
        //{
        //    Debug.Log("12��");
        //}
        Instance = this;

        CreateUIManager();
        CreateCameraManager();

        CreateTimeController();
        CreatePoolManager();
    }



    private GameObject _currentLevel = null;
    private AsyncOperationHandle<GameObject> _stageLoadHandle;
    private void LoadStage(int number)
    {
        OnStageLoadStart?.Invoke();//�������� �ε��� ���۵ȴ�.
        MapManagerInstance = null;
        Addressables.LoadAssetAsync < GameObject>($"Level{number.ToString()}").Completed += //�ε尡 �ٵǸ� �׼��� �����϶�
            (AsyncOperationHandle<GameObject> handle) =>
            {
                if(_currentLevel != null)
                {
                    Destroy(_currentLevel);
                    Addressables.Release(_stageLoadHandle);
                }
                _stageLoadHandle = handle;
                _currentLevel = Instantiate(handle.Result, Vector3.zero, Quaternion.identity);
                StartCoroutine(DelayStart()); //1�ʱ� ��ٷȴٰ� ���� �޼��� ������
            };
    }

    private IEnumerator DelayStart()
    {
        Stage s = _currentLevel.GetComponent<Stage>();

        PlayerTrm.position = s.CannonPos;//ó�� �������� �� �ʻ��� ���� ��ġ�� �̵��ض�.
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

        if(_currentcannonCnt<=0/*||_currentBoxCnt <= 0*/)
        {
            SetGameOver();
        }
        if (_currentBoxCnt == 0)
        {
            _stagenum++;
            LoadStage(_stagenum);
        }
    }

    private void SetGameOver()
    {
        Debug.Log("���� Over");
    }

    private void CreatePoolManager()
    {
        PoolManager.Instance = new PoolManager(transform);
        //���⼭ �ʿ��� �ֵ� Ǯ �������� ��
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


}
