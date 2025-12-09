using UnityEngine;
public class BulletBase : IBullet
{
    protected Vector2 origin;
    private float _damage = 1;
    public float damage { get => _damage; }
    protected Bullet parent;
    public bool collidable => true;
    public float maxLifeTime = 4f;
    protected float currentLifeTime = 0;
    public bool piercing;
    public float speed = 1;
    public float rotationalSpeed = 0;


    public BulletBase(Bullet par) 
    {
        parent = par;
    }
    
    public BulletBase SetSpeed(float s = 1)
    {
        speed = s;
        return this;
    }

    public BulletBase SetDamage(float d = 1)
    { 
        _damage = d;
        return this;
    }

    public BulletBase SetLifetime(float l = 4)
    {
        maxLifeTime = l;
        return this;
    }

    public BulletBase SetRotationalSpeed(float rs = 0)
    {
        rotationalSpeed = rs;
        return this;
    }

    public BulletBase SetPiercing(bool p = false)
    {
        piercing = p;
        return this;
    }

   

    public void Shoot(Vector2 dir, Vector2 originPoint)
    {
        currentLifeTime = 0;
        parent.transform.up = dir;
        origin = originPoint;
    }

    public void OnUpdate(Transform transform)
    {
        currentLifeTime += Time.deltaTime * speed;
        transform.position += Time.deltaTime * transform.up * speed;
        if (currentLifeTime >= maxLifeTime) parent.End();
        if (rotationalSpeed == 0) return;
        transform.Rotate(origin, rotationalSpeed * Time.deltaTime);
    }

    public void EndLife()
    {
        parent.Return();
    }

    public void OnHit(IHittable entity)
    {
        entity.OnHit(damage);
    }

}

public enum DamageTypes
{
    Normal, Fire, Ice, Poison
}
