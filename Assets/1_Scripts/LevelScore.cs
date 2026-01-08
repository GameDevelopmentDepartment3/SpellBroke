using UnityEngine;

public class LevelScore : MonoBehaviour
{
    public static LevelScore instance;

    public int level = 1;
    public float currentXp = 0f;
    public float maxXp = 100f;

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
        xpBar.UpdateUI(currentXp, maxXp, level);
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

    private void LevelUp()
    {
        currentXp -= maxXp;
        level++;

        maxXp = maxXp * 3f / 2f;

        Debug.Log($"레벨업! Lv.{level}");
    }
}