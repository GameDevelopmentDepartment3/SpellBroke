using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AugmentUI : MonoBehaviour
{
    [SerializeField] private GameObject augmentPanel;
    public List<MagicDamage> magics;
    public List<UpgradeData> allUpgrades;
    public List<AugmentButton> upgradeCards;
    public string? mainElement;
    public int curLevel = 0;

    private void Update()
    {
        if (LevelScore.instance.level != this.curLevel)
        {
            if(mainElement == null)
            {
                Debug.Log($"{curLevel} {LevelScore.instance.level}");
                this.curLevel = LevelScore.instance.level;
                Open();
            }
            
        }
    }
    public void Open()
    {
        List<UpgradeData> curLevelUpgrades = new List<UpgradeData>();
        foreach (UpgradeData upgrade in allUpgrades)
        {
            switch (mainElement)
            {
                case "":
                    if (upgrade.levelRequirement == curLevel)
                        curLevelUpgrades.Add(upgrade);
                    break;
                case "Fire":
                    if (upgrade.levelRequirement == curLevel && upgrade.typeOfUpgrade is UpgradeData.UpgradeType.Fire or UpgradeData.UpgradeType.StatUp)
                        curLevelUpgrades.Add(upgrade);
                    break;
                case "Ice":
                    Debug.Log("@@");
                    if (upgrade.levelRequirement == curLevel && upgrade.typeOfUpgrade is UpgradeData.UpgradeType.Ice or UpgradeData.UpgradeType.StatUp)
                        curLevelUpgrades.Add(upgrade);
                    break;
                case "Electric":
                    if (upgrade.levelRequirement == curLevel && upgrade.typeOfUpgrade is UpgradeData.UpgradeType.Electric or UpgradeData.UpgradeType.StatUp)
                        curLevelUpgrades.Add(upgrade);
                    break;
                default:
                    Debug.LogError($"Whar?!?!? mainElement Strange {mainElement} ");
                    break;
            }
        }
        Debug.Log(curLevelUpgrades.Count);
        if (upgradeCards.Count > curLevelUpgrades.Count)
        {
            Debug.LogError("그 레벨의 증강의 개수가 부족합니다.");
            return;
        }
        var result = Enumerable.Range(0, curLevelUpgrades.Count).OrderBy(x => Random.value).Take(3).ToList();
        for (int i = 0; i < upgradeCards.Count; i++)
        {
            //int randomIndex = Random.Range(0, curLevelUpgrades.Count);
            UpgradeData selected = curLevelUpgrades[result[i]];
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