using UnityEngine;
public class Bullet : MonoBehaviour
{

    public IBullet bullet;

    public float speed = 1f;

    public float rotationalSpeed = 0f;

    public float lifetime = 4f;

    public bool piercing = false;

    private Rigidbody2D rb;

    private Pool<Bullet> creator;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHittable target))
        {
            bullet.OnHit(target);
        }
        if (bullet.collidable) bullet.EndLife();
    }

    private void Update()
    {
        bullet.OnUpdate(transform);
    }

    public void End()
    {
        bullet.EndLife();
    }


    public void Fire()
    {
        bullet = new BulletFire(bullet);
    }
    public void Swirl()
    {
        bullet = new BulletSwirly(bullet);
        Debug.Log("s");
    }

    public static void SetState(Bullet b, bool state)
    {
        b.gameObject.SetActive(state);
        if (state)
        {
            b.bullet = new BulletBase(b);
        }
    }

    public virtual void UpdateValues()
    {
        ((BulletBase)bullet).SetSpeed(speed).SetLifetime(lifetime).SetPiercing(piercing);
    }

    public void UpdateSprite(Sprite spr)
    {
        spriteRenderer.sprite = spr;
    }

    public void Shoot(Vector2 dir, Vector2 origin)
    {
        transform.position = origin;
        ((BulletBase)bullet).Shoot(dir, origin);
    }

    public void SetAOE(Pool<AOE> p)
    {
        bullet = new BulletAOE(bullet, p, transform);
        Debug.Log("asd");
    }

    public void SetCreator(Pool<Bullet> p)
    {
        creator = p;
    }

    public void Return()
    {
        Debug.Log("ah");
        creator.Return(this);
    }
    
}
