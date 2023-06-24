using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacer : MonoBehaviour
{
    [SerializeField] private TileBase _tilebase;

    [SerializeField] private float _angle = 0;

    private Tilemap _tilemap;

    private void Awake()
    {
        _tilemap = transform.Find("Tilemap").GetComponent<Tilemap>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _angle = (_angle + 90f) % 360f;
        }
        if (Input.GetMouseButtonDown(0))
        {
            //마우스의 ㅡ월드 좌표를 알아낸다. (3D에서는 이렇게 하면 안된다.)
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = _tilemap.WorldToCell(pos);

            Matrix4x4 rot = Matrix4x4.Rotate(Quaternion.Euler(0, 0, _angle));   

            TileChangeData data = new TileChangeData
            {
                position = cellPos,
                tile = _tilebase,
                transform = rot
            };

            _tilemap.SetTile(data, false);
        }
    }
}
