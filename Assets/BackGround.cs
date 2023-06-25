using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private float _length, startpos;
    [SerializeField]
    private GameObject _cam;
    [SerializeField]
    private float _parallaxEffect;
    void Start()
    {
        startpos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float _dist_x = (_cam.transform.position.x * _parallaxEffect);

        transform.position = new Vector3(startpos + _dist_x, transform.position.y, transform.position.z);
    }
}
