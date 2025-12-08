using UnityEngine;

public class BulletFire : BulletUpgrade
{
    public BulletFire(IBullet bullet) : base(bullet)
    {
    }

    public override float damage => base.damage + 0.2f;


    public override void OnHit(IHittable hit)
    {
        hit.OnFire();
        base.OnHit(hit);
    }
}

public class BulletSwirly : BulletUpgrade
{
    private float _swirlSpeed = 2f * Mathf.PI;
    private Vector3 _swirlVector = Vector3.zero;
    private float _swirlAmplitude = 3f;
    private float _timer = 0;

    public BulletSwirly(IBullet bullet, float speed = 2 * Mathf.PI, float amplitude = 3) : base(bullet)
    {
        _swirlAmplitude = amplitude;
        _swirlSpeed = speed;
        _swirlVector = Vector3.zero;
        _timer = 0;
    }

    public override void OnUpdate(Transform transform)
    {
        base.OnUpdate(transform);
        _timer += Time.deltaTime;
        if (_swirlVector == Vector3.zero) _swirlVector = Vector3.Cross(transform.up, Vector3.forward).normalized;
        transform.position += _swirlVector * Mathf.Sin(_timer * _swirlSpeed + Mathf.PI/2f) * _swirlAmplitude * Time.deltaTime;
    }
}

public class BulletAOE : BulletUpgrade
{
    private Pool<AOE> _AOE;
    private Transform _transform;
    public BulletAOE(IBullet bullet, Pool<AOE> aoe, Transform t) : base(bullet)
    {
        _AOE = aoe;
        _transform = t;
        Debug.Log("asas");
    }

    public override void EndLife()
    {
        Debug.Log("bruieh");
        var a = _AOE.GetObject();
        a.transform.position = _transform.position;
        a.SetCreator(_AOE);
        base.EndLife();
    }



}

public class BulletSeeking : BulletUpgrade
{
    private bool _enemy;
    public BulletSeeking(IBullet bullet, bool enemy) : base(bullet)
    {
        _enemy = enemy;
    }

    public override void OnUpdate(Transform transform)
    {
        Vector2 dir;
        float closestD = Mathf.Infinity;
        if (_enemy) dir = GameManager.instance.player.transform.position - transform.position;
        else
        {
            Transform closest = default;
            foreach (var e in GameManager.instance.activeEnemies)
            {
                var dist = Vector3.Distance(e.transform.position, transform.position);
                if (dist < closestD)
                {
                    closest = e.transform;
                    closestD = dist;
                }
            }

            dir = closest.position - transform.position;
        }
        if(closestD < 4)
        {
            var t = 1f - Mathf.Pow(0.1f, Time.deltaTime);
            transform.up = Vector2.Lerp(transform.up, dir.normalized, t);
        }
        base.OnUpdate(transform);
    }
}
