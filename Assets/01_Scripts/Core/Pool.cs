using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T: PoolableMono
{
    //PoolableMono 상속받은 모든 애들은 T에 올 수 있다.
    private Stack<T> _pool = new Stack<T>();
    private T _prefab;
    private Transform _parent;
    
    public Pool(T prefab, Transform parent, int Count)
    {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < Count; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }
    }

    public T Pop()
    {
        T obj = null;
        if (_pool.Count <= 0)//풀에 지금 남은게 없어 다씀
        {
            obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        else//풀에 남은게 있다면
        {
            obj = _pool.Pop();
            obj.gameObject.SetActive(true);//액티브만 켜서 돌려줘
        }
        return obj;
    }


    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }
}
