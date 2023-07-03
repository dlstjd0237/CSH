using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventTest : MonoBehaviour
{
    private UnityEvent<float, string, GameObject> mangeEvent = null;

    private void Awake()
    {
        mangeEvent?.Invoke(1,"½ËÆÈ",this.gameObject);
    }
}
