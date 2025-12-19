using UnityEngine;
using UnityEngine.UI;

public class ExitScreen : MonoBehaviour, IScreen
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
        SoundSingleton.instance.Button();
        Application.Quit();
    }

    public void No()
    {
        SoundSingleton.instance.Button();
        ScreenManager.instance.Pop();
    }
}
