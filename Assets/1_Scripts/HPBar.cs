using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private DeathUI deathUI;
    [SerializeField] private HitEffect hitEffect;

    private float currentHP;
    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
        UpdateHP();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        hitEffect.PlayHitEffect();
        currentHP = Mathf.Clamp(currentHP - damage, 0f, maxHP);
        UpdateHP();
    }

    private void UpdateHP()
    {
        float ratio = currentHP / maxHP;
        Debug.Log($"HP: {currentHP} / {maxHP} = {ratio}");

        fillImage.fillAmount = ratio;

        if (currentHP <= 0 && !isDead)
        {
            isDead = true;
            deathUI.OnDeath();
        }
    }
}