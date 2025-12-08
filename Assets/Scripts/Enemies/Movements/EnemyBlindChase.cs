using UnityEngine;

public class EnemyBlindChase : IEnemyMovement
{
    public void Move(Transform transform, Rigidbody2D rb, float speed)
    {
        var dir = GameManager.instance.player.transform.position - transform.position;
        rb.velocity = dir.normalized * speed;
    }
}
