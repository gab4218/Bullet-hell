using UnityEngine;

public class EnemySurround : IEnemyMovement
{
    private float _range;
    public EnemySurround(float range = 3)
    {
        _range = range;
    }
    public void Move(Transform transform, Rigidbody2D rb, float speed)
    {
        //Debug.Log(Vector2.Distance(GameManager.instance.player.transform.position, transform.position));
        if (Vector2.Distance(GameManager.instance.player.transform.position, transform.position) < _range)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 1f - Mathf.Pow(0.002f, Time.deltaTime));
            return;
        }
        var dir = GameManager.instance.player.transform.position - transform.position;
        rb.velocity = dir.normalized * speed;
    }
}
