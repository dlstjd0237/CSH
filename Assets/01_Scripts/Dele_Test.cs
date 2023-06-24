using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Dele_Test : MonoBehaviour
{
   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Test());
            
        }
    }
    IEnumerator Test()
    {
    
        yield return new WaitForSeconds(1f);
        
        yield return new WaitForEndOfFrame();
        Debug.Log("3출력");
        yield return null;
        Debug.Log("4출력");
        yield return 4;
        Debug.Log("5출력");
        yield return 5;
        
    }
}
