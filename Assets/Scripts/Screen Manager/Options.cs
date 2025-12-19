using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour, IScreen
{
    private Button[] _buttons;

    public void SetMusic(Toggle slider) => SoundManager.instance.UpdateMusicVolume(slider.isOn? 0 : -50);
    public void SetSFX(Toggle slider) => SoundManager.instance.UpdateSFXVolume(slider.isOn? 0 : -50);
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

    public void Exit() => ScreenManager.instance.Pop();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Exit();
    }
}
