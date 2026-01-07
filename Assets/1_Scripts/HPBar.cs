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

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            TakeDamage(10f);
        }
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
        fillImage.fillAmount = currentHP / maxHP;

        if (currentHP <= 0 && !isDead)
        {
            isDead = true;
            deathUI.OnDeath();
        }
    }
}