using UnityEngine;

public class MagicDamage : MonoBehaviour
{
    public float damage;
    public string attackType;
    public string attackElement;
    private void OnTriggerEnter(Collider other)
    {
        damage = PlayerStatsManager.instance.attack;
        switch (attackType)
        {
            case "ImmediateMagic":
                ImmediatelyMagicAttack(other);
                break;
            case "OverTimeMagic":
                OverTimeMagicAttack(other);
                break;
        }
    }
    public void ImmediatelyMagicAttack(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            ElementAttack(other);
        }
    }
    public void OverTimeMagicAttack(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
    protected virtual void ElementAttack(Collider other)
    {
        if(attackElement == "Electic")
        {
            this.gameObject.GetComponent<ElectricAttack>().enabled = true;
            this.gameObject.GetComponent<ElectricAttack>().hitEnemies.Add(other.gameObject.transform);
        }
    }
}
