using UnityEngine;
using UnityEngine.InputSystem;

public class LevelScore : MonoBehaviour
{
    [SerializeField] private AugmentUI augmentUI;
    public static LevelScore instance;
    
    public int level = 0;
    public float currentXp = 0f;
    public float maxXp = 50f;

    [SerializeField] private XPBar xpBar;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        LevelUp();
        xpBar.UpdateUI(currentXp, maxXp, level);
    }
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            LevelUp();
        }
    }

    public void AddXp(float amount)
    {
        currentXp += amount;

        if (currentXp >= maxXp)
        {
            LevelUp();
        }

        xpBar.UpdateUI(currentXp, maxXp, level);
    }

    void LevelUp()
    {
        level++;
        PlayerStatsManager.instance.LevelUp();
        augmentUI.curLevel = level;
        currentXp = 0;
        maxXp = maxXp * 3f / 2f;
        xpBar.UpdateUI(currentXp, maxXp, level);

        Debug.Log($"레벨업! Lv.{level}");
        augmentUI.Open();
    }
}