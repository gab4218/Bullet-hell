using UnityEngine;

public class EnemyRandomMovement : IEnemyMovement
{
    private Vector3 _randomPos;

    public EnemyRandomMovement()
    {
        _randomPos = new Vector3(Random.Range(GameManager.instance.mapSize.x, GameManager.instance.mapSize.y), Random.Range(GameManager.instance.mapSize.z, GameManager.instance.mapSize.w));
    }

    public void Move(Transform transform, Rigidbody2D rb, float speed)
    {

        if(Vector2.Distance(transform.position, _randomPos) < 1f) _randomPos = new Vector3(Random.Range(GameManager.instance.mapSize.x, GameManager.instance.mapSize.y), Random.Range(GameManager.instance.mapSize.z, GameManager.instance.mapSize.w));

        var dir = _randomPos - transform.position;
        rb.velocity = dir.normalized * speed;
    }
}
