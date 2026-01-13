using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeData/BasicIceMagicUpgrade")]
public class BasicIceMagicUpgrade : UpgradeData
{
    public int decreasingStunStack = 1;
    public float increasingslowAmount = 0.1f;
    public override void ApplyUpgrade(GameObject magic)
    {
        //var iceAttack = magic.GetComponent<IceAttack>();
        //if (iceAttack != null)
        //{
        //    magic.GetComponent<IceAttack>().maxIceStack -= 1;
        //    magic.GetComponent<IceAttack>().slowAmount += increasingslowAmount;
        //}
        UpgradeManager.Instance.minusIceStack += decreasingStunStack;
        UpgradeManager.Instance.plusSlowAmount += increasingslowAmount;
    }
}