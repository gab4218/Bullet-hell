using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{

    public static SaveData data = new SaveData();

    public static void SaveData(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("SaveFile", json);
        Debug.Log(PlayerPrefs.GetString("SaveFile"));
    }

    public static SaveData LoadGame()
    {
        data = new SaveData();
        if (PlayerPrefs.HasKey("SaveFile"))
        {
            data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("SaveFile"));
        }
        Debug.Log(data.money);
        return data;
    }

    public static void DeleteSaveData()
    {
        if (PlayerPrefs.HasKey("SaveFile")) PlayerPrefs.DeleteKey("SaveFile");
    }
}




public struct SaveData
{
    public int money;
    public bool[] unlockedCosmetics;
}
