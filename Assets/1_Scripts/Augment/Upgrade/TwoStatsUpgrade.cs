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
    public float value1;
    public float value2;
    public override void ApplyUpgrade(GameObject magic)
    {
        switch(firstUpgradeType)
        {
            case UpgradeType.HP:
                PlayerStatsManager.instance.AddHP(value1);
                break;
            case UpgradeType.Attack:
                PlayerStatsManager.instance.AddAttack(value1);
                break;
            case UpgradeType.Speed:
                PlayerStatsManager.instance.AddSpeed(value1);
                break;
        }
        switch(secondUpgradeType) 
        {
            case UpgradeType.HP:
                PlayerStatsManager.instance.AddHP(value2);
                break;
            case UpgradeType.Attack:
                PlayerStatsManager.instance.AddAttack(value2);
                break;
            case UpgradeType.Speed:
                PlayerStatsManager.instance.AddSpeed(value2);
                break;
        }
    }
}
