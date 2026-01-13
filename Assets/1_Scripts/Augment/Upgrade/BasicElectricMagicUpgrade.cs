using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeData/BasicElectricMagicUpgrade")]
public class BasicElectricMagicUpgrade : UpgradeData
{
    public float increasingChainRadius = 10f;
    public int increasingMaxChainCounts = 1;
    public override void ApplyUpgrade(GameObject magic)
    {
        var electricAttack = magic.GetComponent<ElectricAttack>();
        if (electricAttack != null)
        {
            electricAttack.chainRadius += increasingChainRadius;
            electricAttack.maxChainCounts += increasingMaxChainCounts;
        }
    }
}
