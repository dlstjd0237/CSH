using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;
    public void SetTimeFreeze(float freezeValue, float beforeDelay, float freeaeTime)
    {
        StopAllCoroutines();
        StartCoroutine(TimeFreezeCoroutine(freezeValue, beforeDelay, () =>
        {
            StartCoroutine(TimeFreezeCoroutine(1f, freeaeTime));
        }));
    }
    private IEnumerator TimeFreezeCoroutine(float freezeValue, float beforeDelay, Action Callback = null)
    {
        yield return new WaitForSecondsRealtime(beforeDelay);
        Time.timeScale = freezeValue;
        Callback?.Invoke();
    }
}
