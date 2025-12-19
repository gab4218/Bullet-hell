using UnityEngine;
using UnityEngine.UI;

public class DeleteScreen : MonoBehaviour, IScreen
{
    public void Activate()
    {
        foreach (Button b in GetComponentsInChildren<Button>()) b.enabled = true;
    }

    public void Deactivate()
    {
        foreach (Button b in GetComponentsInChildren<Button>()) b.enabled = false;
    }

    public void Free()
    {
        Destroy(gameObject);
    }

    public void DeleteAll()
    {
        SaveManager.DeleteSaveData();
        SaveData data = SaveManager.LoadGame();
        Debug.Log("deletedAll");
        Menu.loaded = true;
        MoneyManager.money = data.money;
        InventoryManager.unlockedItems = data.unlockedCosmetics;
        SoundSingleton.instance.Button();
        ScreenManager.instance.Pop();
    }

    public void No()
    {
        SoundSingleton.instance.Button();
        ScreenManager.instance.Pop();
    }
}
