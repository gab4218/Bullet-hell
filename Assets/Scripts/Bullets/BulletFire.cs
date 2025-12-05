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
