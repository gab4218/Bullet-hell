using UnityEngine;

public class PlayerModel
{
    private Transform _tranform;

    private Rigidbody2D _rb;

    private float _speed;

    private float _baseDamage;

    private int _maxHealth;

    private int _currentHealth;

    public PlayerModel(Transform tranform, Rigidbody2D rb)
    {
        _tranform = tranform;
        _rb = rb;
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

    public PlayerModel SetMaxHealth(int health)
    {
        _maxHealth = health;
        _currentHealth = health;
        return this;
    }

    public void Move(Vector2 dir) => _rb.velocity = dir.normalized * _speed;

    public void Shoot(Vector2 dir)
    {

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
