using UnityEngine;

public class Factory<T> where T : MonoBehaviour
{
    public T prefab;

    public virtual T Spawn() => GameObject.Instantiate(prefab);
}
