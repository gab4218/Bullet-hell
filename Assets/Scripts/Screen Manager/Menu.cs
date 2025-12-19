using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string _optionsName = "Options";
    [SerializeField] private string _shopName = "Shop";
    [SerializeField] private AudioClip _badClip;
    [SerializeField] private TMP_Text _moneyText;
    public static bool loaded = false;

    private void Awake()
    {
        if (loaded) return;

        Debug.Log("loading");
        SaveData data = SaveManager.LoadGame();
        loaded = true;
        MoneyManager.money = data.money;
        InventoryManager.unlockedItems = data.unlockedCosmetics;

    }

    public void Exit() => ScreenManager.instance.Push("ExitScreen");

    public void Tutorial()
    {
        AsyncLoadManager.instance.LoadScene("Tutorial");
    }

    private void Start()
    {
        ScreenManager.instance.Push(new ScreenGO(transform));
        if (MoneyManager.newMoney > 0) ScreenManager.instance.Push("Money");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("loading");
            SaveData data = SaveManager.LoadGame();
            loaded = true;
            MoneyManager.money = data.money;
            InventoryManager.unlockedItems = data.unlockedCosmetics;
        }
        //_moneyText.text = MoneyManager.money.ToString();
    }

    public void DeleteAll()
    {
        ScreenManager.instance.Push("Delete");

    }

    public void Options() => ScreenManager.instance.Push(_optionsName);

    public void Shop() => ScreenManager.instance.Push(_shopName);

    public void Play()
    {
        if (StaminaManager.instance.currentStamina <= 0)
        {
            SoundSingleton.instance.sfxSource.PlayOneShot(_badClip);
        }
        else
        {
            StaminaManager.instance.UseStamina();
            AsyncLoadManager.instance.LoadScene("Game");
        }
    }


    private void OnApplicationQuit() => Save();

    private void OnApplicationPause(bool pause)
    {
        if (pause) Save();
    }

    public void Pause() => ScreenManager.instance.Push("Pause");

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) Save();
    }

    private void Save()
    {
        SaveData data = new();
        data.money = MoneyManager.money;
        data.unlockedCosmetics = InventoryManager.unlockedItems;
        SaveManager.SaveData(data);
    }

}
