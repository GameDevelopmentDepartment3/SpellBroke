using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeData/TwoStatsUpgrade")]
public class TwoStatsUpgrade : UpgradeData
{
    public float attackBonus;
    public enum UpgradeType
    {
        HP,
        Attack,
        Speed
    }
    public UpgradeType firstUpgradeType;
    public UpgradeType secondUpgradeType;
    public float value;
    public override void ApplyUpgrade(GameObject magic)
    {
        switch(firstUpgradeType)
        {
            case UpgradeType.HP:
                PlayerStatsManager.instance.AddHP(value);
                break;
            case UpgradeType.Attack:
                PlayerStatsManager.instance.AddAttack(value);
                break;
            case UpgradeType.Speed:
                PlayerStatsManager.instance.AddSpeed(value);
                break;
        }
        switch(secondUpgradeType) 
        {
            case UpgradeType.HP:
                PlayerStatsManager.instance.AddHP(value);
                break;
            case UpgradeType.Attack:
                PlayerStatsManager.instance.AddAttack(value);
                break;
            case UpgradeType.Speed:
                PlayerStatsManager.instance.AddSpeed(value);
                break;
        }
    }
}
