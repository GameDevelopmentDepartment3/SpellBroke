using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeData/BasicIceMagicUpgrade")]
public class BasicIceMagicUpgrade : UpgradeData
{
    public float decreasingStunStack = 1f;
    public float increasingslowAmount = 0.1f;
    public override void ApplyUpgrade(GameObject magic)
    {
        var iceAttack = magic.GetComponent<IceAttack>();
        if (iceAttack != null)
        {
            iceAttack.maxIceStack -= 1;
            iceAttack.slowAmount += increasingslowAmount;
        }
    }
}