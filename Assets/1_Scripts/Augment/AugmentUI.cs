using UnityEngine;

public class AugmentUI : MonoBehaviour
{
    [SerializeField] private GameObject augmentPanel;

    public void Open()
    {
        Time.timeScale = 0f;          // 게임 정지
        augmentPanel.SetActive(true);
    }

    public void Close()
    {
        Time.timeScale = 1f;          // 게임 재개
        augmentPanel.SetActive(false);
    }
}