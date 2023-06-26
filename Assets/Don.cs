using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Don : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
