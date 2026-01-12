using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeData/BasicFireMagicUpgrade")]
public class BasicFireMagicUpgrade : UpgradeData
{
    public float increasingmaxBurnStack = 1f;
    public float increasingBurnDamage = 4f;
    public override void ApplyUpgrade(GameObject magic)
    {
        var fireAttack = magic.GetComponent<FireAttack>();
        if (fireAttack != null)
        {
            fireAttack.maxBurnStack += 1;
            fireAttack.burnDamagePerSecond += increasingBurnDamage;
        }
    }
}
