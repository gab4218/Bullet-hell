using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IHittable
{
    private float _speed = 2f;
    private float _cooldown = 1f;
    private float _bulletSpeed = 5f;
    private float _coinDropChance = 0.05f;
    public float maxHP = 4;
    public float currentHP = 4;
    private Sprite _sprite;
    private SpriteRenderer _sRenderer;
    [SerializeField] private Animator _anim;
    private IEnemyMovement _movement;
    private IEnemyAttack _attack;
    private Rigidbody2D _rb;
    private Collider2D _circleCollider;
    private Pool<EnemyBase> _parent;
    private GameObject _coin;
    private float _baseHP;

    public EnemyBase SetSpeed(float speed)
    {
        _speed = speed;
        return this;
    }

    public EnemyBase SetSprite(Sprite s)
    {
        _sprite = s;
        if (_sprite != default && _sRenderer != default) _sRenderer.sprite = _sprite;
        return this;
    }

    public EnemyBase SetAnimator(RuntimeAnimatorController c)
    {
        if (_anim == null) _anim = GetComponent<Animator>();
        _anim.runtimeAnimatorController = c;
        return this;
    }

    public EnemyBase SetCooldown(float cd)
    {
        _cooldown = cd;
        return this;
    }

    public EnemyBase SetBaseHP(float hp)
    {
        _baseHP = hp;
        return this;
    }

    public EnemyBase SetHPMultiplier(float hpMult)
    {
        maxHP = _baseHP * hpMult * EnemyScaler.instance.hpMult;
        currentHP = maxHP;
        return this;
    }

    public EnemyBase SetCoinDropChance(float c)
    {
        _coinDropChance = c;
        Debug.Log("coi " + _coinDropChance);
        return this;
    }

    public EnemyBase SetCoin(GameObject c)
    {
        _coin = c;
        return this;
    }

    public EnemyBase SetBehaviour(IEnemyMovement m, IEnemyAttack a)
    {
        _movement = m;
        _attack = a;
        return this;
    }


    void Start()
    {
        _sRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        //_attack = new EnemyRadialAttack();
        //_movement = new EnemyBlindChase();
        _circleCollider = GetComponent<Collider2D>();
    }

    
    void Update()
    {
        _movement.Move(transform, _rb, _speed);
        _attack.Attack(transform, _cooldown, _bulletSpeed, _anim);
    }

    public static void SetState(EnemyBase e, bool state)
    {
        e.gameObject.SetActive(state);
        if (state)
        {
            GameManager.instance.activeEnemies.Add(e);
        }
        else
        {
            GameManager.instance.activeEnemies.Remove(e);
        }
    }

    public void SetCreator(Pool<EnemyBase> p) => _parent = p;

    public void OnHit(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            if (Random.Range(0, 1f) <= _coinDropChance) Instantiate(_coin, transform.position, Quaternion.identity);
            GameManager.instance.enemyDeathCount++;
            if (_attack is EnemySeekingBurst) GameManager.instance.eyeDeathCount++;
            _parent.Return(this);
        }
    }
}
