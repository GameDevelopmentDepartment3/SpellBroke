using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    static public PlayerStatsManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public float maxHP = 100f;
    public float attack = 10f;
    public float moveSpeed = 5f;
    public void AddHP(float value)
    {
        maxHP += value;
        Debug.Log($"체력 증가: {maxHP}");
    }

    public void AddAttack(float value)
    {
        attack += value;
        Debug.Log($"공격력 증가: {attack}");
    }

    public void AddSpeed(float value)
    {
        moveSpeed += value;
        Debug.Log($"이속 증가: {moveSpeed}");
    }
    public void LevelUp()
    {
        maxHP *= 1.1f;
        attack *= 1.1f;
        moveSpeed *= 1.05f;
    }
}