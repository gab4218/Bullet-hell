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
        EventManager.Subscribe(EventType.RoundStart, OnRoundClear);
    }

    private void OnRoundClear(params object[] par)
    {
        if(RoundManager.instance.round % 4 == 0) hpMult *= scalingFactor;
        enemyCurrency += RoundManager.instance.round / 2;
    }
}
