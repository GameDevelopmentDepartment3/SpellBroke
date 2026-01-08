using UnityEngine;

public class MagicDamage : MonoBehaviour
{
    public float damage;
    public string attackType;
    private void OnTriggerEnter(Collider other)
    {
        switch (attackType)
        {
            case "ImmediateMagic":
                ImmediatelyMagicAttack(other);
                break;
            case "OverTimeMagic":
                OverTimeMagicAttack();
                break;
        }
    }
    public void ImmediatelyMagicAttack(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        Destroy(this);
    }
    public void OverTimeMagicAttack()
    {

    }
}
