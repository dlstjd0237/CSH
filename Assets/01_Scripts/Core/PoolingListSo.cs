using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //인스펙터에서 나오도록 직렬화 시키고 
public struct PoolingPair
{
    public PoolableMono Prefab;
    public int Count;
}
[CreateAssetMenu (menuName = "SO/PoolList")]
public class PoolingListSo : ScriptableObject
{
    public List<PoolingPair> Pairs;
}
