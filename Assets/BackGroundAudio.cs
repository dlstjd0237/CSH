using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
 
    public void SetBackGeound(float num)
    {
        PlayerPrefs.SetFloat("BackGroundAudio", num);
        PlayerPrefs.GetFloat("BackGroundAudio");
        _audioSource.volume = num;
    }
}
