using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorInOut : MonoBehaviour
{
    [SerializeField]
    private Image _fadeImager;

    public void In()
    {
        FadeIn();
    }

    private IEnumerator FadeIn()
    {
        _fadeImager.color = new Color(_fadeImager.color.r, _fadeImager.color.g, _fadeImager.color.b, 0);
        while (_fadeImager.color.a < 1)
        {
            Debug.Log("¾ÆÀÕ");
            _fadeImager.color = new Color(_fadeImager.color.r, _fadeImager.color.g, _fadeImager.color.b, _fadeImager.color.a + (Time.deltaTime / 2));
            yield return null;
        }
        Loading();
    }
    private void Loading()
    {
        LoadingSceneController.LoadScene("GameScene");
    }
}
