using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleButton : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Click()
    {
        StartCoroutine(Co());
    }
    IEnumerator Co()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(1.5f);
        LoadingSceneController.LoadScene("StartScene");

    }
}
