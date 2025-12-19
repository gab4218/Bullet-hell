using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeOption : MonoBehaviour
{
    public RewardTypes reward;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    private Dictionary<RewardTypes, string> _descriptions = new Dictionary<RewardTypes, string>()
    {
        {RewardTypes.Damage, "Damage"},
        {RewardTypes.Swirl, "Swirl"},
        {RewardTypes.Speed, "Speed"},
        {RewardTypes.Seeking, "Homing"},
        {RewardTypes.Resistance, "Protection"},
        {RewardTypes.Piercing, "Piercing"},
        {RewardTypes.AOE, "Exploding"},
        {RewardTypes.BulletCount, "Multishot"},
        {RewardTypes.BulletSpeed, "Trowel Speed"},
        {RewardTypes.Range, "Range"},
        {RewardTypes.Firerate, "Fire rate"},
        {RewardTypes.MaxHealth, "Max health"},
        {RewardTypes.Heal, "Healing"}
    };

    public void ChangeValues(RewardTypes r, Sprite newImage)
    {
        reward = r;
        _image.sprite = newImage;
        _text.text = _descriptions[r];
    }
}
