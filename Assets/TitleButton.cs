using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleButton : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private Image _softImage;
    private bool _inOut;
    private void Awake()
    {
        StartCoroutine(ColorIn());
        _audioSource = GetComponent<AudioSource>();
    }

    public void Click()
    {
        StartCoroutine(Co());
    }
    IEnumerator Co()
    {
        BackGroundAudio _back = FindAnyObjectByType<BackGroundAudio>();
        _back.StopAudio();
        _audioSource.Play();
        _inOut = false;
        _softImage.raycastTarget = true;
        _softImage.color = new Color(_softImage.color.r, _softImage.color.g, _softImage.color.b, 0);
        while (_softImage.color.a < 1)
        {
            _softImage.color = new Color(_softImage.color.r, _softImage.color.g, _softImage.color.b, _softImage.color.a + (Time.deltaTime / 2));
            yield return null;
        }
        LoadingSceneController.LoadScene("StartScene");

    }
    IEnumerator ColorIn()
    {
        _inOut = true;
        _softImage.color = new Color(_softImage.color.r, _softImage.color.g, _softImage.color.b, 1);

        while (_softImage.color.a > 0)
        {
            if (!_inOut)
            {
                break;
            }
            _softImage.color = new Color(_softImage.color.r, _softImage.color.g, _softImage.color.b, _softImage.color.a - (Time.deltaTime / 2));
            yield return null;
        }

    }
}
