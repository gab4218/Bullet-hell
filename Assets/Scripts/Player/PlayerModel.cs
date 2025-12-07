using UnityEngine;

public class PlayerModel
{
    public Transform tranform;

    private Rigidbody2D _rb;

    private float _speed;

    private float _baseDamage;

    private float _attackCooldown;

    private float _currentCooldown;

    public bool canAttack { get => _currentCooldown >= _attackCooldown; }

    public bool swirly = false;

    public bool aoe = false;

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
        _attackCooldown = 0.5f;
        _currentCooldown = 0.5f;
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
        _currentHealth += health - _maxHealth;
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

    public void Move(Vector2 dir) => _rb.velocity = dir.normalized * _speed;

    

    public void Shoot(Vector2 dir)
    {
        if (!canAttack) return;
        _currentCooldown = 0;
        var bullet = GameManager.instance.playerBulletPool.GetObject();
        if (aoe) bullet.speed = 1f; else bullet.speed = 2;
        bullet.UpdateValues();
        bullet.Shoot(dir, tranform.position);
        bullet.SetCreator(GameManager.instance.playerBulletPool);
        switch (_damageType)
        {
            case DamageTypes.Normal:
                break;
            case DamageTypes.Fire:
                bullet.Fire();
                break;
            case DamageTypes.Ice:
                break;
            case DamageTypes.Poison:
                break;
            default:
                break;
        }
        if (swirly) bullet.Swirl();
        if (aoe) bullet.SetAOE(GameManager.instance.playerAOEPool);

    }

    public void OnUpdate()
    {
        _currentCooldown += Time.deltaTime;
    }

    public void Hurt(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) EventManager.TriggerEvent(EventType.Death);
    }

    public void Die()
    {

    }

}


