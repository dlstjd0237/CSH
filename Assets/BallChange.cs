using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallType
{
    BALL,
    SKULL,
    HOUND,
    HERO,
    WOMAN_HERO
}

public class BallChange : MonoBehaviour
{
    private SpriteRenderer _sprite;

    [SerializeField]
    private Sprite[] image;

    private BallType _ballType = BallType.BALL;

    public static Action<String> ChangeBall;
    private void Awake()
    {
        ChangeBall += Change;
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {

        BallImageChange();
    }
    public void Change(String name)
    {
        switch (name)
        {
            case "Ball":
                _ballType = BallType.BALL; break;
            case "Skull":
                _ballType = BallType.SKULL; break;
            case "Hound":
                _ballType = BallType.HOUND; break;
            case "Hero":
                _ballType = BallType.HERO; break;
            case "Woman_Hero":
                _ballType = BallType.WOMAN_HERO;break;

        }

    }
    private void BallImageChange()
    {
        switch (_ballType)
        {
            case BallType.BALL:
                SpriteChange(0); break;
            case BallType.SKULL:
                SpriteChange(1); break;
            case BallType.HOUND:
                SpriteChange(2); break;
            case BallType.HERO:
                SpriteChange(3); break;
            case BallType.WOMAN_HERO:
                SpriteChange(4); break;
        }
    }
    private void SpriteChange(int num)
    {
        _sprite.sprite = image[num];
    }
}
