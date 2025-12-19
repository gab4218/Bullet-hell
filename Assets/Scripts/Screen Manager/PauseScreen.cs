using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour, IScreen
{
    private Button[] _buttons;

    [SerializeField] private string _optionsName = "Options";
    [SerializeField] private string _menuName = "Menu";

    public static bool paused = false;

    private bool _enabled = false;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();

        foreach (Button button in _buttons)
        {
            button.interactable = false;
        }
        paused = true;
    }

    public void Activate()
    {
        foreach (Button button in _buttons)
        {
            button.interactable = true;
        }
        _enabled = true;
    }

    public void Deactivate()
    {
        foreach (Button button in _buttons)
        {
            button.interactable = false;
        }
        _enabled = false;
    }

    public void Free()
    {
        paused = false;
        Destroy(gameObject);
    }

    public void Options()
    {
        SoundSingleton.instance.Button();
        ScreenManager.instance.Push(_optionsName);
    }

    public void Menu()
    {
        SoundSingleton.instance.Button();
        ScreenManager.instance.Push(_menuName);
    }

    public void Resume()
    {
        SoundSingleton.instance.Button();
        ScreenManager.instance.Pop();
    }

    private void Update()
    {
        if (_enabled)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) ScreenManager.instance.Pop();
        }
    }

    //public void ChangeLanguage() =>

}


