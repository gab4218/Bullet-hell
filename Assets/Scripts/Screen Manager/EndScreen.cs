using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour, IScreen
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] private TMP_Text _coinText;

    public void Activate()
    {
        foreach (var button in _buttons)
        {
            button.interactable = true;
        }
        _coinText.text = MoneyManager.newMoney.ToString();
    }

    public void Deactivate()
    {
        foreach (var button in _buttons)
        {
            button.interactable = false;
        }
    }

    public void Free()
    {
        Destroy(gameObject);
    }

    public void Menu()
    {
        SoundSingleton.instance.Button();
        AsyncLoadManager.instance.LoadScene("Menu");
    }
}
