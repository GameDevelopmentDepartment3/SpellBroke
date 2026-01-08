using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        HPBar hpBar = FindObjectOfType<HPBar>();

        if (hpBar != null)
        {
            hpBar.TakeDamage(damage);
        }
        else
        {
            Debug.LogError("HPBar를 찾지 못함");
        }
    }
}