using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectary : MonoBehaviour
{
    private float _time;
    private int _count;

    [SerializeField]
    private GameObject _projectaryPrefab;
    private List<Transform> _projectileList = new List<Transform>();

    [SerializeField] private LayerMask _whatIsObstacle;
    private float _delta = 0;
    public void SetData(float time, int count)
    {

        _time = time;
        _count = count;
        for (int i = 0; i < count; i++)
        {
            GameObject g = Instantiate(_projectaryPrefab, transform);
            _projectileList.Add(g.transform);
            g.SetActive(false);
        }
        _delta = _time / _count;
    }


    public void DrawLine(Vector2 pos, Vector3 power)
    {
        bool flag = true;
        float gravity = Physics2D.gravity.y;

        for (int i = 0; i < _projectileList.Count; i++)
        {
                Transform t = _projectileList[i];
            if (flag)
            {

                t.gameObject.SetActive(true);


                Vector2 dotPos;
                float time = _delta * i;
                dotPos.x = pos.x + power.x * time;
                dotPos.y = pos.y + power.y * time + (gravity * Mathf.Pow(time, 2)) * 0.5f;

                Collider2D col =Physics2D.OverlapCircle(dotPos, 0.3f, _whatIsObstacle);

                if (col != null)
                {
                    flag = false;
                }
                

                t.position = dotPos;
            }
            else
            {
                t.gameObject.SetActive(false);
            }
        }
    }
}
