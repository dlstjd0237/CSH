using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
    Transform _square;
    float _angle = 0;
    private void Awake()
    {
        _square = transform.Find("Square");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _angle += 45f;

            float rad = 30 * Mathf.Deg2Rad;
            float c = Mathf.Cos(rad);
            float s = Mathf.Sin(rad);

            Matrix4x4 mat = new Matrix4x4(
                new Vector4(c, -s, 0, 0),
                new Vector4(s, c, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1)
                );

            Vector3 pos = _square.localPosition;
            _square.localPosition = mat.MultiplyPoint(pos);
        }
    }
}
