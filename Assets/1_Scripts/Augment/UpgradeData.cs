using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    [TextArea]
    public string description;
    public Sprite icon;
    
    public enum UpgradeType
    {
        HP,
        Attack,
        Speed
    }
    public UpgradeType upgradeType;
    public float value;
}
