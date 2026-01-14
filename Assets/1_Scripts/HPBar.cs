using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HPBar : MonoBehaviour
{ 
    [SerializeField] private TextMeshProUGUI damageText;
    
    private float beforeMaxHp = 100f;
    [SerializeField] private Image fillImage;
    [SerializeField] public float maxHP = 100f;
    [SerializeField] private DeathUI deathUI;
    [SerializeField] private HitEffect hitEffect;
    [SerializeField] private TextMeshProUGUI HealthText;

    private float currentHP;
    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
        UpdateHP();
    }
    private void Update()
    {
        if (PlayerStatsManager.instance.maxHP != this.maxHP)
        {
            Heal(PlayerStatsManager.instance.maxHP - this.maxHP);
            maxHP = PlayerStatsManager.instance.maxHP;
        }
    }
    public void TakeDamage(float damage)
    {
        if (isDead) return;

        hitEffect.PlayHitEffect();
        currentHP = Mathf.Clamp(currentHP - damage, 0f, maxHP);
        UpdateHP();
    }
    public void Heal(float healAmount)
    {
        if (isDead) return;
        currentHP = Mathf.Clamp(currentHP + healAmount, 0f, maxHP);
        UpdateHP();
    }

    private void UpdateHP()
    {
        float ratio = currentHP / maxHP;
        Debug.Log($"HP: {currentHP} / {maxHP} = {ratio}");

        fillImage.fillAmount = ratio;

        HealthText.text = $"{Mathf.CeilToInt(currentHP)} / {Mathf.CeilToInt(maxHP)}";
        
        if (currentHP <= 0 && !isDead)
        {
            isDead = true;
            deathUI.OnDeath();
        }
    }
}