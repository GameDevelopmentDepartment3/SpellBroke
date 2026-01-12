using UnityEngine;

//[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public abstract class UpgradeData : ScriptableObject
{
    public string upgradeName;
    [TextArea]
    public string description;
    public Sprite icon;
    public int levelRequirement;
    public enum UpgradeType
    {
        StatUp,
        Fire,
        Ice,
        Electric
    }
    public UpgradeType typeOfUpgrade;

    //public enum UpgradeType
    //{
    //    HP,
    //    Attack,
    //    Speed,
    //    Magic
    //}
    //public UpgradeType upgradeType;
    //public float value;
    public abstract void ApplyUpgrade(GameObject magic);
}
