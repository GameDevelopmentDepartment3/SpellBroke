using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    // 현재 게임 판에서 누적된 업그레이드 수치들
    public int minusIceStack = 0;
    public float plusSlowAmount = 0;
    public int plusBurnStack = 0;
    public float plusBurnDamagePerSecond = 0;
    public float plusElectricChainRange = 0;
    public int plusElectricChainCount = 0;

    private void Awake()
    {
        Instance = this;
        // 여기서 초기화하면 게임을 껐다 켤 때마다 깨끗하게 시작됩니다.
        ResetUpgrades();
    }

    public void ResetUpgrades()
    {
        minusIceStack = 0;
        plusSlowAmount = 0;
        plusBurnStack = 0;
        plusBurnDamagePerSecond = 0;
        plusElectricChainCount = 0;
        plusElectricChainRange = 0;
    }
}
