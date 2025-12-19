using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMain : MonoBehaviour, IHittable
{
    private PlayerModel _model;
    private IController _controller;
    private PlayerView _view;
    public Rigidbody2D rb;
    [SerializeField] private int _maxHP;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage = 1;
    [SerializeField] private Sprite[] _bulletSprites;
    [SerializeField] private Image _hpImage;
    [SerializeField] private Image _maxHpImage;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _hpSize = 77;
    [SerializeField] private JoystickInput _attack, _move;
    [SerializeField] private AudioClip _hurtSound, _throwSound;


    public bool hasAOE => _model.aoe;

    public bool hasSwirl => _model.swirly;

    public bool hasSeek => _model.seeking;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _model = new PlayerModel(transform, rb).SetSprites(_bulletSprites).SetMaxHealth(_maxHP).SetBaseDamage(_damage).SetSpeed(_speed);
        _view = new PlayerView(_model, GetComponent<Animator>()).SetHpSize(_hpSize).SetHpImages(_hpImage, _maxHpImage).SetSound(_throwSound);
#if UNITY_STANDALONE_WIN
        _controller = new PlayerControllerPC(_model, _view);
        _move.transform.parent.gameObject.SetActive(false);
        _attack.transform.parent.gameObject.SetActive(false);
#else
        _controller = new PlayerControllerMobile(_model, _view).SetInputs(_move, _attack);
#endif
    }
   

    
    void Update()
    {
        _controller.OnUpdate();
        _model.OnUpdate();
        _view.CheckHealth();
    }

    public void UpgradeDamage()
    {
        _model.AddDamage(1);
    }

    public void UpgradeFireRate()
    {
        _model.AddFirerate(2);
    }

    public void UpgradeRange()
    {
        _model.AddRange(2f);
    }

    public void UpgradeMultiShot()
    {
        _model.AddBullet();
    }

    public void UpgradeBulletSpeed()
    {
        _model.AddBulletSpeed(1f);
    }

    public void UpgradeSpeed()
    {
        _model.AddSpeed(1f);
    }

    public void UpgradeResistance()
    {
        _model.AddProtection();
    }

    public void AOE()
    {
        _model.SetAOE();
    }

    public void Piercing()
    {
        _model.Piercing();
    }
    public void Swirly()
    {
        _model.SetSwirl();
    }

    public void Seeking()
    {
        _model.SetSeek();
    }

    public void UpgradeHealth()
    {
        _model.AddHP(2);
    }

    public void Heal(int h)
    {
        _model.Heal(h);
    }

    public void OnHit(float dmg)
    {
        _model.Hurt((int)dmg);
        _spriteRenderer.color = Color.red;
        SoundSingleton.instance.sfxSource.PlayOneShot(_hurtSound);
        Invoke("DefaultColor", 0.1f);
    }

    private void DefaultColor()
    {
        _spriteRenderer.color = Color.white;
    }
}
