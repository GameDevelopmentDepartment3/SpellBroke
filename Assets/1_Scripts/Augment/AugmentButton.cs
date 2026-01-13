using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AugmentButton : MonoBehaviour
{
    private UpgradeData upgradeData;
    public Image icon;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI DescriptionText;
    public GameObject magic;

    [SerializeField] private PlayerStatsManager player;
    [SerializeField] private AugmentUI augmentUI;
    public void SetUp(UpgradeData newData)
    {
        this.upgradeData = newData;
        this.icon.sprite = newData.icon;
        this.TitleText.text = newData.upgradeName;
        this.DescriptionText.text = newData.description;
    }
    public void OnClick()
    {
        if (augmentUI.curLevel == 1)
        {
            switch (upgradeData.typeOfUpgrade)
            {
                case UpgradeData.UpgradeType.Fire:
                    augmentUI.mainElement = "Fire";
                    break;
                case UpgradeData.UpgradeType.Ice:
                    augmentUI.mainElement = "Ice";
                    break;
                case UpgradeData.UpgradeType.Electric:
                    augmentUI.mainElement = "Electric";
                    break;
                default:
                    Debug.LogError($"{upgradeData.typeOfUpgrade} <----- 이렇게 이상한 값 드감");
                    break;
            }
        }
        upgradeData.ApplyUpgrade(magic);
        augmentUI.Close();
    }
}