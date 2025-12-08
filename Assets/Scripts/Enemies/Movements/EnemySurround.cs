using UnityEngine;

public class EnemySurround : IEnemyMovement
{
    private float _range;
    public EnemySurround(float range = 4)
    {
        _range = range;
    }
    public void Move(Transform transform, Rigidbody2D rb, float speed)
    {
        if (Vector2.Distance(GameManager.instance.player.transform.position, transform.position) < _range) return;
        var dir = GameManager.instance.player.transform.position - transform.position;
        rb.velocity = dir.normalized * speed;
    }
}
