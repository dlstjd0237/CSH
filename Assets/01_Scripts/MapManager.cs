using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private Tilemap _groundTile;

    private void Awake()
    {
        _groundTile = transform.Find("StageTilemap/Ground").GetComponent<Tilemap>();

    }
    public void CheckDestroy(Vector3 worldPos, float expRadius)
    {
        //worldPos �ֺ����� expRadius��ŭ �׶��� Ÿ���� ������ �װ� SetNull�� �����ش�.
        Vector3Int cellPos = _groundTile.WorldToCell(worldPos);
        int r = Mathf.FloorToInt(expRadius);//����
        for (int i = -r ; i < r; i++)
        {
            for (int j = -r ; j < r; j++)
            {
                if (Mathf.Abs(i) + Mathf.Abs(j)>r) continue;
                Vector3Int offset = new Vector3Int(j, i);

                var targetPos = cellPos + offset;
                var tile = _groundTile.GetTile(targetPos);
                if(tile != null)
                {
                    _groundTile.SetTile(targetPos, null);
                }
            }
            
        }
      
        //for (int i = -r + 1; i < r; i++)
        //{
        //    for (int j = -r + 1; j < r; j++)
        //    {
        //        var targetPos = cellPos + new Vector3Int(j, i);
        //        var tile = _groundTile.GetTile(targetPos);
        //        if (tile != null)
        //        {
        //            _groundTile.SetTile(targetPos, null);
        //        }
        //    }

        //}
    }
}
