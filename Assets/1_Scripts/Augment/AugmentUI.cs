using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class AugmentUI : MonoBehaviour
{
    [SerializeField] private GameObject augmentPanel;
    public List<MagicDamage> magics;
    public List<UpgradeData> allUpgrades;
    public List<AugmentButton> upgradeCards;
    public int curLevel = 0;


    public void Open()
    {
        List<UpgradeData> curLevelUpgrades = new List<UpgradeData>();
        foreach (UpgradeData upgrade in allUpgrades)
        {
            if (upgrade.levelRequirement == curLevel)
                curLevelUpgrades.Add(upgrade);
        }
        if(upgradeCards.Count < curLevelUpgrades.Count)
        {
            Debug.LogError("그 레벨의 증강의 개수가 부족합니다.");
            return;
        }
        for (int i = 0; i < upgradeCards.Count; i++)
        {
            //int randomIndex = Random.Range(0, curLevelUpgrades.Count);
            UpgradeData selected = curLevelUpgrades[i];
            foreach(MagicDamage magic in magics)
            {
                string fir = magic.attackElement1;
                string sec = magic.attackElement2;
                if (fir == selected.typeOfUpgrade.ToString() || sec == selected.typeOfUpgrade.ToString())
                {
                    switch(selected.typeOfUpgrade.ToString())
                    {
                        case "Fire":
                            upgradeCards[i].magic = magic.burn;
                            break;
                        case "Ice":
                            upgradeCards[i].magic = magic.ice;
                            break;
                        case "Electric":
                            upgradeCards[i].magic = magic.gameObject;
                            break;
                    }
                }
            }
            upgradeCards[i].SetUp(selected);
            //curLevelUpgrades.Remove(selected);
        }
        Time.timeScale = 0f;          // 게임 정지
        augmentPanel.SetActive(true);
    }

    public void Close()
    {
        Time.timeScale = 1f;          // 게임 재개
        augmentPanel.SetActive(false);
    }
}