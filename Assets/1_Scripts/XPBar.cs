using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class XPBar : MonoBehaviour
{
    public int level = 1;

    [SerializeField] private Image fillImage;
    public float currentXp = 0f;
    public float maxXp = 100f;   // 시작 최대 XP
    public TextMeshProUGUI text;
    private void Start()
    {
        AddXp(0);
    }

    /// 경험치 추가
    public void AddXp(float amount)
    {
        currentXp += amount;
        UpdateXP();
    }

    void Update()
    {
        // q & e  테스트
        if (Keyboard.current.numpadPlusKey.wasPressedThisFrame)
        {
            AddXp(10f);
        }
    }

    /// 레벨 업 처리
    void LevelUp()
    {
        level++;

        currentXp = 0f;

        // 다음 maxXp = 이전 maxXp * 3 / 2
        maxXp = maxXp * 3f / 2f;

        text.text = $"{level}";

        Debug.Log($"레벨 업! 현재 레벨: {level}, 다음 maxXp: {maxXp}");
    }
    private void UpdateXP()
    {
        fillImage.fillAmount = currentXp / maxXp;

        if (currentXp >= maxXp)
        {
            currentXp = 0;
            UpdateXP();
            LevelUp();
        }
    }
}