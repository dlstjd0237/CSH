using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //�ν����Ϳ��� �������� ����ȭ ��Ű�� 
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
