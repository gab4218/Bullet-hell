using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T>
{
    public delegate T FactoryMethod();

    private FactoryMethod _factory;

    private Action<T, bool> _setState;

    private List<T> _pool = new();

    public Pool(FactoryMethod factory, Action<T, bool> enabler, int startCount = 100)
    {
        _factory = factory;
        _setState = enabler;

        for(int i = 0; i < startCount; i++)
        {
            var obj = _factory();

            _setState(obj, false);

            _pool.Add(obj);
        }
    }

    public T GetObject()
    {
        T obj = default;
        
        if(_pool.Count > 0)
        {
            obj = _pool[0];

            _pool.RemoveAt(0);
        }
        else
        {
            obj = _factory();
        }

        _setState(obj, true);

        return obj;
    }

    public void Return(T obj)
    {
        _setState(obj, false);

        _pool.Add(obj);
    }
}
