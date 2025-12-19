using UnityEngine;

public interface IEnemyAttack
{
    public void Attack(Transform transform, float cooldown, float speed, Animator anim);
}
