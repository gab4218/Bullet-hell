using UnityEngine;

public interface IBullet
{
    public float damage { get; }
    public bool collidable { get; }
    public void EndLife();
    public void OnHit(IHittable hit);
    public void OnUpdate(Transform transform);
}
