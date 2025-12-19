using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuConfirmScreen : MonoBehaviour, IScreen
{
    private Button[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();

        foreach (Button button in _buttons)
        {
            button.interactable = false;
        }
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

    public void Yes()
    {
        PauseScreen.paused = false;
        SoundSingleton.instance.Button();
        AsyncLoadManager.instance.LoadScene("Menu");
    }

    public void No()
    {
        SoundSingleton.instance.Button();
        ScreenManager.instance.Pop();
    }
}
