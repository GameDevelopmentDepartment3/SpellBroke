using UnityEngine;

public class MagicDamage : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
