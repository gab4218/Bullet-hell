using UnityEngine;

public class EnemyScaler : MonoBehaviour
{
    public static EnemyScaler instance;

    public float hpMult = 1;

    public int enemyCurrency = 5;

    public float scalingFactor = 1.1f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        EventManager.Subscribe(EventType.RoundClear, OnRoundClear);
    }

    private void OnRoundClear(params object[] par)
    {
        hpMult *= scalingFactor;
        enemyCurrency += RoundManager.instance.round;
    }
}
