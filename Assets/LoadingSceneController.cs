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

    [SerializeField]
    private string[] _tip;
    [SerializeField]
    private TMP_Text _tipText;

    [SerializeField]
    private Image _blackPanel;

    public static void LoadScene(string sceneName)
    {
        _nextScene = sceneName;
        SceneManager.LoadScene("LoadScene");
    }
    private void Start()
    {
        StartCoroutine(ImageColorSetStart());
        ChangeTipText();
        StartCoroutine(LoadSceneProcess());
    }
    private void ChangeTipText()
    {
        int num = Random.Range(0, _tip.Length);
        _tipText.text = "<bounce>" + _tip[num]+ "</bounce>";
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
                _timer += Time.unscaledDeltaTime*0.25f;
                _bar.fillAmount = Mathf.Lerp(0f, 1f, _timer);
                _gayG.text = (int)_gayGTimer + "%";
                if (_bar.fillAmount >= 1f)
                {
                    _gayG.text = "100%";
                    _blackPanel.color = new Color(_blackPanel.color.r, _blackPanel.color.g, _blackPanel.color.b, 0);
                    while (_blackPanel.color.a < 1)
                    {
                        _blackPanel.color = new Color(_blackPanel.color.r, _blackPanel.color.g, _blackPanel.color.b, _blackPanel.color.a + (Time.deltaTime / 2));
                        yield return null;
                    }
                    op.allowSceneActivation = true;
                    yield break;
                }
            }

        }
    }
    private IEnumerator ImageColorSetStart()
    {
        _blackPanel.color = new Color (_blackPanel.color.r, _blackPanel.color.g, _blackPanel.color.b,1);
        while (_blackPanel.color.a > 0)
        {
            _blackPanel.color = new Color(_blackPanel.color.r, _blackPanel.color.g, _blackPanel.color.b, _blackPanel.color.a-(Time.deltaTime/2));
            yield return null;
        }
    }
    private IEnumerator ImageCOlorSetEnd()
    {
        _blackPanel.color = new Color(_blackPanel.color.r, _blackPanel.color.g, _blackPanel.color.b, 0);
        while (_blackPanel.color.a < 1)
        {
            _blackPanel.color = new Color(_blackPanel.color.r, _blackPanel.color.g, _blackPanel.color.b, _blackPanel.color.a + (Time.deltaTime / 2));
            yield return null;
        }

    }
}
