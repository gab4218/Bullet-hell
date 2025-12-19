using UnityEngine;
public class Bullet : MonoBehaviour
{

    public IBullet bullet;

    public float speed = 1f;

    public float rotationalSpeed = 0f;

    public float lifetime = 4f;

    public bool piercing = false;

    private Rigidbody2D _rb;

    private Pool<Bullet> _creator;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); 
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHittable target))
        {
            bullet.OnHit(target);
        }
        if (!piercing) bullet.EndLife();
        
    }

    private void Update()
    {
        bullet.OnUpdate(transform);
    }

    public void End()
    {
        bullet.EndLife();
    }

    public void SetDamage(float d)
    {
        ((BulletBase)bullet).SetDamage(d);
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
            b.piercing = false;
        }
    }

    public virtual void UpdateValues()
    {
        ((BulletBase)bullet).SetSpeed(speed).SetLifetime(lifetime).SetPiercing(piercing);
    }

    public void UpdateSprite(Sprite spr)
    {
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = spr;
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
    public void Seeking(bool enemy = false)
    {
        bullet = new BulletSeeking(bullet, enemy);
        Debug.Log("ssds");
    }
    public void SetCreator(Pool<Bullet> p)
    {
        _creator = p;
    }

    public void Return()
    {
        Debug.Log("ah");
        _creator.Return(this);
    }
    
}
