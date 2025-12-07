using UnityEngine;

public class Factory<T> where T : MonoBehaviour
{
    public T prefab;

    public virtual T Spawn() => GameObject.Instantiate(prefab);

    public virtual Pool<T>.FactoryMethod SetPrefab(T p)
    {
        prefab = p;
        return Spawn;
    }
}
