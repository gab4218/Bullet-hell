using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    private PlayerModel _model;
    private PlayerController _controller;
    public Rigidbody2D rb;
    [SerializeField] private int _maxHP;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage = 1;
    [SerializeField] private Sprite[] _bulletSprites;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _model = new PlayerModel(transform, rb).SetSprites(_bulletSprites).SetMaxHealth(_maxHP).SetBaseDamage(_damage).SetSpeed(_speed);
        _controller = new PlayerController(_model);
        
    }
   

    
    void Update()
    {
        _controller.OnUpdate();
        _model.OnUpdate();
    }

    public void UpgradeDamage()
    {
        _model.AddDamage(1);
    }

    public void UpgradeFireRate()
    {
        _model.AddFirerate(1);
    }

    public void UpgradeRange()
    {
        _model.AddRange(1f);
    }

    public void UpgradeMultiShot()
    {
        _model.AddBullet();
    }

    public void UpgradeBulletSpeed()
    {
        _model.AddBulletSpeed(0.25f);
    }

    public void UpgradeSpeed()
    {
        _model.AddSpeed(0.25f);
    }

    public void UpgradeResistance()
    {

    }

    public void UpgradeHealth()
    {
        _model.AddHP(1);
    }

    public void Heal(int h)
    {
        _model.Heal(h);
    }
}
