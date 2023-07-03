using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    private bool _first = false;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_first == false)
        {
            _first = true;
        _audioSource.Play();
        }
    }
 
    public void SetBackGeound(float num)
    {
        PlayerPrefs.SetFloat("BackGroundAudio", num);
        _audioSource.volume = num;
    }

    public void StopAudio()
    {
        _audioSource.Stop();
    } 
}
