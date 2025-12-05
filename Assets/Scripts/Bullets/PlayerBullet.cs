using UnityEngine;
public class PlayerBullet : MonoBehaviour
{

    private IBullet _bullet;
    


    private void OnTrigger(Collider2D other)
    {
        if (other.TryGetComponent(out IHittable target) && target is not PlayerMain)
        {
            _bullet.OnHit(target);
        }
        if (_bullet.collidable) _bullet.EndLife();
    }
}
