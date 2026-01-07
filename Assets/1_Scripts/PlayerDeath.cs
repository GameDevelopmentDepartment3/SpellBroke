using UnityEngine;
using UnityEngine.Rendering;

public class PlayerDeath : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject deathPanel;

    [Header("Post Processing")]
    [SerializeField] private Volume blurVolume;

    private bool isDead = false;

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // 1. 게임 멈춤
        Time.timeScale = 0f;

        // 2. 블러 켜기
        if (blurVolume != null)
            blurVolume.enabled = true;

        // 3. UI 표시
        deathPanel.SetActive(true);
    }
}