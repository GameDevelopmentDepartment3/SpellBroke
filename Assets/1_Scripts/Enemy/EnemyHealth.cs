using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHP = 100f;
    private float currentHP;
    public string EnemyName;
    public float Hp
    {
        get { return currentHP; }
        set
        {
            if (value <= 0f)
            {
                SummonManager.instance.returnObject(EnemyName, this.gameObject);
            }
            else if (value > maxHP)
                currentHP = maxHP;
            else
                currentHP = value;
            Debug.Log($"Enemy HP: {currentHP}/{maxHP}");
            Die();
            
        }

    }
    void Start()
    {
        currentHP = maxHP;
    }
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            TakeDamage(10f);
        }
    }
    public void TakeDamage(float damage)
    {
        Hp -= damage;
    }

    private void Die()
    {
        SummonManager.instance.DropXP(gameObject);
        SummonManager.instance.returnObject(EnemyName, gameObject);
    }
}
