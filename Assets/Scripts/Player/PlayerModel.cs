using UnityEngine;

public class PlayerModel
{
    public Transform tranform;

    private Rigidbody2D _rb;

    private float _speed;

    private float _baseDamage;

    private float _attackCooldown;

    private float _currentCooldown;

    private float _bulletSpeed;

    private float _bulletRange;

    private float _bulletSpacing;

    private float _firerate;

    private int _bulletCount;

    private float _firerateMult;

    private float _blockChance;

    private int _blockStacks;

    public bool canAttack { get => _currentCooldown >= _attackCooldown; }

    public bool swirly = false;

    public bool aoe = false;

    public bool seeking = false;

    private int _maxHealth;

    private int _currentHealth;

    private Sprite[] _bulletSprites;

    private DamageTypes _damageType = DamageTypes.Normal;

    public PlayerModel(Transform tranform, Rigidbody2D rb)
    {
        this.tranform = tranform;
        _rb = rb;
        _speed = 4;
        _baseDamage = 1;
        _maxHealth = 10;
        _currentHealth = 10;
        _firerate = 
        _attackCooldown = 0.5f;
        _currentCooldown = 0.5f;
        _firerateMult = 1;
        _bulletSpeed = 2f;
        _bulletRange = 4f;
        _bulletCount = 1;
        _bulletSpacing = 120f / _bulletCount;
        _blockChance = 0;
        _blockStacks = 0;
        swirly = false;
    }

    public PlayerModel SetSprites(Sprite[] spr)
    {
        _bulletSprites = spr;
        return this;
    }
    public PlayerModel SetSpeed(float speed = 4)
    {
        _speed = speed;
        return this;
    }

    public PlayerModel SetBaseDamage(float baseDamage = 1)
    {
        _baseDamage = baseDamage;
        return this;
    }

    public PlayerModel SetMaxHealth(int health = 10)
    {
        _currentHealth = _maxHealth;
        _maxHealth = health;
        return this;
    }

    public PlayerModel SetCooldown(float cd = 0.5f)
    {
        _attackCooldown = cd;
        return this;
    }

    public void SetDamageType(DamageTypes type)
    {
        _damageType = type;
    }

    public void Heal(int health)
    {
        _currentHealth += Mathf.Min(health, _maxHealth - _currentHealth);
    }

    public void Move(Vector2 dir) => _rb.velocity = dir.normalized * _speed;

    

    public void Shoot(Vector2 dir)
    {
        if (!canAttack) return;
        _currentCooldown = 0;

        for(int i = 0; i < _bulletCount; i++)
        {
            Vector2 direction = dir;

            Quaternion rot = Quaternion.AngleAxis((_bulletCount % 2) == 0 ? ((i - _bulletCount / 2) * _bulletSpacing + _bulletSpacing/2f) : (_bulletSpacing * (i - _bulletCount / 2)), Vector3.forward);

            direction = rot *  direction;
            CreateBullet(direction);
        }
        

    }

    public void CreateBullet(Vector2 dir)
    {
        var bullet = GameManager.instance.playerBulletPool.GetObject();
        bullet.speed = _bulletSpeed;
        bullet.lifetime = _bulletRange;
        bullet.SetDamage(_baseDamage);
        bullet.UpdateValues();
        bullet.Shoot(dir.normalized, tranform.position);
        bullet.SetCreator(GameManager.instance.playerBulletPool);

        if (swirly) bullet.Swirl();
        if (aoe) bullet.SetAOE(GameManager.instance.playerAOEPool);
        if (seeking) bullet.Seeking();
    }

    public void OnUpdate()
    {
        _currentCooldown += Time.deltaTime;
    }

    public void Hurt(int damage)
    {
        if (Random.Range(0, 100f) < _blockChance) return;
        _currentHealth -= damage;
        if (_currentHealth <= 0) EventManager.TriggerEvent(EventType.Death);
    }

    public void Die()
    {

    }

    public void AddRange(float r)
    {
        _bulletRange += r;
    }

    public void SetAOE()
    {
        aoe = true;
        _bulletRange /= 2f;
        _bulletSpeed /= 2f;
        _firerateMult /= 2f;
    }

    public void SetSwirl()
    {
        swirly = true;
        _bulletSpeed *= 1.25f;
        _bulletRange *= 1.25f;
        _firerateMult *= 1.25f;
        _firerate *= _firerateMult;
        _attackCooldown = 1f / _firerate;
        _bulletCount = Mathf.Max(_bulletCount/3, 1);
    }

    public void AddBullet()
    {
        _bulletCount++;
        _bulletSpacing = 120f / _bulletCount;
    }

    public void AddSpeed(float s)
    {
        _speed += s;
    }

    public void AddBulletSpeed(float s)
    {
        _bulletSpeed += s;
    }

    public void AddHP(int hp)
    {
        _maxHealth += hp;
        _currentHealth += hp;
    }

    public void AddFirerate(float b)
    {
        _firerate += b * _firerateMult;
        _attackCooldown = 1f / _firerate;
    }

    public void AddDamage(float d)
    {
        _baseDamage += d;
    }

    public void AddProtection()
    {
        _blockStacks++;
        for(int i = 0; i < _blockStacks; i++)
        {
            _blockChance = Mathf.Lerp(_blockChance, 100f, 0.05f);
        }
    }
}


