using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LoadingSceneController : MonoBehaviour
{
    static string _nextScene;

    [SerializeField]
    private Image _bar;

    [SerializeField]
    private TMP_Text _gayG;

    public static void LoadScene(string sceneName)
    {
        _nextScene = sceneName;
        SceneManager.LoadScene("LoadScene");
    }
    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(_nextScene);
        op.allowSceneActivation = false;
        float _gayGTimer;

        float _timer = 0f;
        while (!op.isDone)
        {
            yield return null;


            _gayGTimer = _timer * 100;
            if (op.progress < 0.9f)
            {
                _gayG.text = (int)_gayGTimer + "%";
                _bar.fillAmount = op.progress;
            }
            else
            {
                _timer += Time.unscaledDeltaTime*0.5f;
                _bar.fillAmount = Mathf.Lerp(0f, 1f, _timer);
                _gayG.text = (int)_gayGTimer + "%";
                if (_bar.fillAmount >= 1f)
                {
                    _gayG.text = "100%";
                    op.allowSceneActivation = true;
                    yield break;
                }
            }

        }
    }
}
