using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private string[] _tutorialScreens;
    [SerializeField] private Sprite _enemyBulletSprite;
    [SerializeField] private RuntimeAnimatorController _enemyAnim;
    [SerializeField] private Transform _spawnPoint;
    private bool _item = true;
    public static TutorialManager instance;
    public int index = 0;
    private bool _last = false;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    void Start()
    {
        var eb = GameManager.instance.enemyPool.GetObject();
        eb.SetBehaviour(new EnemyStatic(), new EnemyConstantSpin(_enemyBulletSprite)).SetCoinDropChance(0).SetBaseHP(5).SetAnimator(_enemyAnim).SetHPMultiplier(1f).SetCooldown(0.5f).SetCreator(GameManager.instance.enemyPool);
        eb.transform.position = _spawnPoint.position;
        ScreenManager.instance.Push(_tutorialScreens[0]);
    }

    private void Update()
    {
        if (index == 0 && GameManager.instance.player.transform.position.x < _spawnPoint.transform.position.x + 5) NextScreen();
        if (GameManager.instance.activeEnemies.Count <= 0 && _item)
        {
            _item = false;
            ScreenManager.instance.Push("RoundClear");
            NextScreen();
        }
        if (GameManager.instance.activeEnemies.Count <= 0 && _last)
        {
            _last = false;
            NextScreen();
        }
    }

    public void NextScreen()
    {
        index++;
        ScreenManager.instance.Push(_tutorialScreens[index]);
    }

    public void Last()
    {
        _last = true;
    }
    
}
