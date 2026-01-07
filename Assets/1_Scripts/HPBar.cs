using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float maxHP = 100f;

    private float currentHP;

    void Start()
    {
        currentHP = maxHP;
        UpdateHP();
    }

    void Update()
    {
        // q & e 체력 테스트
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            TakeDamage(10f);
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            TakeDamage(-10f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0f, maxHP);
        UpdateHP();
    }

    private void UpdateHP()
    {
        fillImage.fillAmount = currentHP / maxHP;
    }
}