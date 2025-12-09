using UnityEngine;

public class EnemyTeleporter : IEnemyMovement
{
    private float _cooldown;
    private float _t = 0;
    public EnemyTeleporter(float cooldown = 3f)
    {
        _cooldown = cooldown;
        _t = 0;
    }

    public void Move(Transform transform, Rigidbody2D rb, float speed)
    {
        _t += Time.deltaTime * speed;
        if (_t < _cooldown) return;
        _t = 0;
        transform.position = new Vector2(Random.Range(GameManager.instance.mapSize.x, GameManager.instance.mapSize.y), Random.Range(GameManager.instance.mapSize.z, GameManager.instance.mapSize.w));
        if (Vector2.Distance(transform.position, GameManager.instance.player.transform.position) <= 2) Move(transform, rb, speed);
    }
}
