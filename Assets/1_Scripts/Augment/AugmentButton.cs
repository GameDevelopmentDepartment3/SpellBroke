using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AugmentButton : MonoBehaviour
{
    public enum AugmentType
    {
        HP,
        Attack,
        Speed
    }
    public AugmentType type;
    private UpgradeData upgradeData;
    public Image icon;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI DescriptionText;
    public GameObject magic;

    [SerializeField] private PlayerStatsManager player;
    [SerializeField] private AugmentUI augmentUI;
    public void SelectAugment()
    {
        switch (type)
        {
            case AugmentType.HP:
                PlayerStatsManager.instance.AddHP(20f);
                break;
            
            case AugmentType.Attack:
                PlayerStatsManager.instance.AddAttack(10f);
                break;

            case AugmentType.Speed:
                PlayerStatsManager.instance.AddSpeed(5f);
                break; 
        }
        augmentUI.Close();
    }
    public void SetUp(UpgradeData newData)
    {
        this.upgradeData = newData;
        this.icon.sprite = newData.icon;
        this.TitleText.text = newData.upgradeName;
        this.DescriptionText.text = newData.description;
    }
    public void OnClick()
    {
        upgradeData.ApplyUpgrade(magic);
        augmentUI.Close();
    }
}