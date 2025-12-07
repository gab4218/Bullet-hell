using UnityEngine;

public interface IEnemyMovement
{
    public void Move(Transform transform, Rigidbody2D rb, float speed);
}
