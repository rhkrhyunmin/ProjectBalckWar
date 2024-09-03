using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableMono
{
    private Queue<T> _pool = new Queue<T>();
    private T _prefab;
    private PoolType _poolType;
    private Transform _parent;

    public Pool(T prefab, PoolType poolType, Transform parent, int itemAmount = 20)
    {
        _poolType = poolType;
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < itemAmount; ++i)
        {
            T item = GameObject.Instantiate(prefab, parent);
            item.poolType = poolType;
            item.gameObject.name = _poolType.ToString();

            Push(item);
        }
    }

    public void Push(T item)
    {
        if (!Application.isPlaying)
        {
            return;
        }

        item.gameObject.SetActive(false);
        _pool.Enqueue(item);
    }

    public T Pop()
    {
        T item;

        if (_pool.Count <= 0)
        {
            item = GameObject.Instantiate(_prefab, _parent);
            item.poolType = _poolType;
            item.gameObject.name = _poolType.ToString();
        }
        else
        {
            item = _pool.Dequeue();

            item.gameObject.SetActive(true);
        }

        return item;
    }
}
