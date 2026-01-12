using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeData/OneStatupgrade")]
public class OneStatUpgrade : UpgradeData
{
    public enum UpgradeType
    {
        HP,
        Attack,
        Speed
    }
    public UpgradeType upgradeType;
    public float value;
    public override void ApplyUpgrade(GameObject magic)
    {
        switch(upgradeType)
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
