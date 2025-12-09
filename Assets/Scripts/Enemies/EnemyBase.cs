using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private float _speed = 2f;
    private float _cooldown = 1f;
    private float _bulletSpeed = 2f;
    private float _hpMultiplier = 1f;
    private Sprite _sprite;
    private SpriteRenderer _sRenderer;
    private IEnemyMovement _movement;
    private IEnemyAttack _attack;
    private Rigidbody2D _rb;
    private CircleCollider2D _circleCollider;

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

    public EnemyBase SetCooldown(float cd)
    {
        _cooldown = cd;
        return this;
    }

    public EnemyBase SetHPMultiplier(float hpMult)
    {
        _hpMultiplier = hpMult;
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
        _attack = new EnemyRadialAttack();
        _movement = new EnemyBlindChase();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    
    void Update()
    {
        _movement.Move(transform, _rb, _speed);
        _attack.Attack(transform, _cooldown, _bulletSpeed);
        transform.position += Separate(GameManager.instance.activeEnemies, _circleCollider.radius + 0.1f);
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

    private Vector3 Separate(List<EnemyBase> enemies, float radius)
    {
        Vector3 desiredVel = Vector3.zero;
        foreach (EnemyBase eb in enemies)
        {
            if (eb == this) continue;

            var dir = transform.position - eb.transform.position;
            if (dir.sqrMagnitude > radius * radius) continue;

            desiredVel += dir;
        }

        if (desiredVel == Vector3.zero) return desiredVel;

        desiredVel = desiredVel.normalized * _speed;

        return desiredVel;

    }
}
