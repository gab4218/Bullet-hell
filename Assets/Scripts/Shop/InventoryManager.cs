using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static bool[] unlockedItems = new bool[]{false, false};

    public static InventoryManager instance;

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
    }

    private void Start()
    {
        if (unlockedItems == default) unlockedItems = new bool[2] { false, false };
        Debug.Log(unlockedItems);
    }

    
}
