using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asdf : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.x > 6 || transform.position.y < -21)
        {
            PlayerProfile pl = FindAnyObjectByType<PlayerProfile>();
            pl.SetVisible(true);
        }
        else
        {
            PlayerProfile pl = FindAnyObjectByType<PlayerProfile>();
            pl.SetVisible(false);
        }
    }
}
