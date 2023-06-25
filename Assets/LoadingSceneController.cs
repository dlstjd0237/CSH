using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingSceneController : MonoBehaviour
{
    static string _nextScene;

    [SerializeField]
    private Image _bar; 

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

        float _timer = 0f;
        while (!op.isDone)
        {
            yield return null;
            
            if(op.progress < 0.9f)
            {
                _bar.fillAmount = op.progress; 
            }
            else
            {
                _timer += Time.unscaledDeltaTime;
                _bar.fillAmount = Mathf.Lerp(0.9f, 1f, _timer);
                if(_bar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
