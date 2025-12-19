using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyConfirmScreen : MonoBehaviour, IScreen
{
    private Button[] _buttons;
    [SerializeField] private TMP_Text _text;

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
        _text.text = MoneyManager.newMoney.ToString();
        MoneyManager.money += MoneyManager.newMoney;
        MoneyManager.newMoney = 0;
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

    public void Ok()
    {
        ScreenManager.instance.Pop();
        MoneyManager.money += MoneyManager.newMoney;
        MoneyManager.newMoney = 0;
    }

    public void Dupe()
    {
        AdManager.instance.Dupe();
        ScreenManager.instance.Pop();
    }
}
