using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class XPBar : MonoBehaviour
{
    public int level = 1;

    public float currentXp = 0f;
    public float maxXp = 100f;   // 시작 최대 XP
    
    /// 경험치 추가
    public void AddXp(float amount)
    {
        currentXp += amount;

        if (currentXp >= maxXp)
        {
            LevelUp();
        }
    }

    void Update()
    {
        // q & e  테스트
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            currentXp -= 10;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentXp += 10;
        }
    }

    /// 레벨 업 처리
    void LevelUp()
    {
        level++;

        currentXp = 0f;

        // 다음 maxXp = 이전 maxXp * 3 / 2
        maxXp = maxXp * 3f / 2f;

        Debug.Log($"레벨 업! 현재 레벨: {level}, 다음 maxXp: {maxXp}");
    }
}