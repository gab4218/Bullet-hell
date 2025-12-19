using UnityEngine;

public class StartingScreen : MonoBehaviour
{
    [SerializeField] private Transform _startingScreen;

    private void Start()
    {
        ScreenManager.instance.Push(new ScreenGO(_startingScreen));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseScreen.paused)
            {
                ScreenManager.instance.Clear();
            }
            else
            {
                ScreenManager.instance.Push("Pause");
            }
        }
    }

    private void OnApplicationQuit() => Save();

    private void OnApplicationPause(bool pause)
    {
        if(pause) Save();
    }

    public void Pause() => ScreenManager.instance.Push("Pause");

    private void OnApplicationFocus(bool focus)
    {
        if(!focus) Save();
    }

    private void Save()
    {
        SaveData data = new();
        data.money = MoneyManager.money;
        data.unlockedCosmetics = InventoryManager.unlockedItems;
        SaveManager.SaveData(data);
    }
}
