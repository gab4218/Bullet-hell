using UnityEngine;
using UnityEngine.UI;

public class PlayerView
{
    private PlayerModel _model;

    private Animator _anim;

    private float _hpSize;

    private Image _hpImage;

    private Image _maxHpImage;

    public PlayerView(PlayerModel model, Animator anim)
    {
        _model = model;
        _anim = anim;
    }

    public PlayerView SetHpSize(float s)
    {
        _hpSize = s;
        return this;
    }

    public PlayerView SetHpImages(Image img, Image img2)
    {
        _hpImage = img;
        _maxHpImage = img2;
        return this;
    }

    public void Move(Vector2 dir)
    {
        bool b = dir.magnitude > 0;
        _anim.SetBool("Moving", b);
        if (b)
        {
            if(Mathf.Abs(dir.x) > 0) _anim.SetFloat("X", dir.x);
            _anim.SetFloat("Y", dir.y);
        }
    }

    public void Attack()
    {
        _anim.SetTrigger("Hit");
    }

    public void CheckHealth()
    {
        _hpImage.rectTransform.sizeDelta = new Vector2(_hpSize * _model.currentHealth, _hpImage.rectTransform.sizeDelta.y);
        _maxHpImage.rectTransform.sizeDelta = new Vector2(_hpSize * _model.maxHealth + 1.5f * _hpSize, _maxHpImage.rectTransform.sizeDelta.y);
        
    }
}
