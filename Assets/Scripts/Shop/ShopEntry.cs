using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopEntry : MonoBehaviour
{
    [SerializeField] private float _priceMult = 1;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _button;
    public int price;
    public int item;
    private void Start()
    {
        if (InventoryManager.unlockedItems == default || InventoryManager.unlockedItems.Length <= 0) InventoryManager.unlockedItems = new bool[2] { false, false };
        if (InventoryManager.unlockedItems[item])
        {
            Destroy(_priceText.gameObject);
            Destroy(this);
        }
        price = (int)(ShopManager.instance.defaultPrice * _priceMult);
        ShopManager.instance.OnPriceChange += ChangePrice;
        _priceText.text = "Price: " + price;
    }

    private void ChangePrice()
    {
        price = (int)(ShopManager.instance.defaultPrice * _priceMult);
        _priceText.text = "Price: " + price;
    }

    private void Update()
    {
        _button.interactable = price <= MoneyManager.money || DevModeDetector.instance.devMode;
    }

    public void Buy()
    {
        MoneyManager.instance.Purchase(this);
        if (InventoryManager.unlockedItems[item])
        {
            Destroy(_priceText.gameObject);
            Destroy(_button.gameObject);
            Destroy(this);
        }
    }
}
