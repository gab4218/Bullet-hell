using UnityEngine;

public class EnemyStatic : IEnemyMovement
{
    public void Move(Transform transform, Rigidbody2D rb, float speed)
    {
        rb.velocity = Vector3.zero;
        return;
    }
}
