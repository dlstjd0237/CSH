using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeBallController : MonoBehaviour
{
    [SerializeField]
    private List<Image> _image = new List<Image>();

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
    public void Hero()
    {
        if (GameManager.Instance.Lvel4)
        {
            GameManager.Instance.expRadius = 4;
            BallChange.ChangeBall("Hero");
        }
        else { Nope(); }
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.Lvel2) ChangeColor(0);
        if (GameManager.Instance.Lvel3) ChangeColor(1);
        if (GameManager.Instance.Lvel4) ChangeColor(2);
    }
    private void ChangeColor(int num)
    {
        Color qwer = _image[num].color;
        qwer = new Color(0.2619817f, 1, 0, 1);
        _image[num].color = qwer;
    }
    private void Nope()
    {
        CameraManneger.Instance.ShakeCam(2f, 20);
        CameraManneger.Instance.ShakeCam2(0.6f, 10);
    }
}
