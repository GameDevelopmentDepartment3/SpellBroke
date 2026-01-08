using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBar : MonoBehaviour
{
    public static event Action OnLevelUp;
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI levelText;
    
    public void UpdateUI(float currentXp, float maxXp, int level)
    {
        fillImage.fillAmount = currentXp / maxXp;
        levelText.text = level.ToString();
    }

    
}