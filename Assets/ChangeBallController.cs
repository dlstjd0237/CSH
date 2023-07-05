using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBallController : MonoBehaviour
{
    public void Ball()
    {
        GameManager.Instance.expRadius = 1;
        BallChange.ChangeBall("Ball");
    }
    public void Skull()
    {
        if (GameManager.Instance.Lvel2)
        {
            GameManager.Instance.expRadius = 2;
            BallChange.ChangeBall("Skull");
        }
        else { Nope(); }
    }
    public void Hound()
    {
        if (GameManager.Instance.Lvel3)
        {
            GameManager.Instance.expRadius = 3;
            BallChange.ChangeBall("Hound");
        }
        else { Nope(); }
    }
    private void Nope()
    {
        CameraManneger.Instance.ShakeCam(2f, 20);
        CameraManneger.Instance.ShakeCam2(0.6f, 10);
    }
}
