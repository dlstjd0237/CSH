using System;
using UnityEngine;


public class GGm<T>
{
   public  T a;

}

public class Test : MonoBehaviour
{
    private void Start()
    {
        GGm<int> b = new GGm<int>();
        b.a = 2;
    }
}
