using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour, IScreen
{
    private Button[] _buttons;

    [SerializeField] Toggle[] _toggles;

    public void SetMusic(Toggle slider) => SoundManager.instance.UpdateMusicVolume(slider.isOn? -50 : 0);
    public void SetSFX(Toggle slider) => SoundManager.instance.UpdateSFXVolume(slider.isOn? -50 : 0);
    public void SetMaster(Slider slider) => SoundManager.instance.UpdateMasterVolume(slider.value);

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();

        foreach (Button button in _buttons)
        {
            button.interactable = false;
        }
    }

    public void Save()
    {
        //SaveData data = new();
        //data.money = MoneyManager.money;
        //data.unlockedCosmetics = InventoryManager.unlockedCosmetics;
        ////ChartDataHolder.instance.Save();
        //string[] ac = new string[ChartDataHolder.allCharts.Count];
        //for (int i = 0; i < ac.Length; i++)
        //{
        //    ac[i] = JsonUtility.ToJson(ChartDataHolder.allCharts[i]);
        //}
        //data.allCharts = ac;
        //SaveManager.SaveData(data);
    }

    public void Activate()
    {
        foreach (Button button in _buttons)
        {
            button.interactable = true;
        }
        if (SoundManager.instance.GetSFXVolume() < 0) _toggles[0].isOn = true;
        if (SoundManager.instance.GetMusicVolume() < 0) _toggles[1].isOn = true;
    }

    public void Deactivate()
    {
        foreach (Button button in _buttons)
        {
            button.interactable = false;
        }
    }

    public void Free()
    {
        Destroy(gameObject);
    }

    public void Exit()
    {
        SoundSingleton.instance.Button();
        ScreenManager.instance.Pop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Exit();
    }
}
