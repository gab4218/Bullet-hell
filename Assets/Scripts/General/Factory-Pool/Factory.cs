using UnityEngine;

public class Factory<T> where T : MonoBehaviour
{
    public T prefab;
    private Transform _parent;

    public virtual T Spawn()
    {
        if (_parent == null) return GameObject.Instantiate(prefab);
        else return GameObject.Instantiate(prefab, _parent);
    }

    public virtual Pool<T>.FactoryMethod SetPrefab(T p)
    {
        prefab = p;
        return Spawn;
    }

    public virtual Factory<T> SetParent(Transform t = null)
    {
        _parent = t;
        return this;
    }
}
