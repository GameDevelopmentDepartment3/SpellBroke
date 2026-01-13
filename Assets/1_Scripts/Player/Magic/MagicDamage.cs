using UnityEngine;

public class MagicDamage : MonoBehaviour
{
    public float damage;
    public string attackType;
    public string attackElement1;
    public string attackElement2;
    public GameObject burn;
    public GameObject ice;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
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
        if(attackElement1 == "Electric" || attackElement2 == "Electric")
        {
            this.gameObject.GetComponent<ElectricAttack>().enabled = true;
            this.gameObject.GetComponent<ElectricAttack>().hitEnemies.Add(other.gameObject.transform);
        }
        if(attackElement1 == "Fire" || attackElement2 == "Fire")
        {
            foreach (Transform child in other.transform)
            {
                if (child.GetComponent<FireAttack>() != null)
                {
                    child.GetComponent<FireAttack>().currentBurnStack++;
                    return;
                }
            }
            var fireEffect = Instantiate(burn, other.transform);
            fireEffect.transform.SetParent(other.transform);
            Destroy(this.gameObject.GetComponent<Collider>());
        }
        if(attackElement1 == "Ice" || attackElement2 == "Ice"){
            foreach(Transform child in other.transform)
            {
                if (child.GetComponent<IceAttack>() != null)
                {
                    child.GetComponent<IceAttack>().currentIceStack++;
                    return;
                }
            }
            var iceEffect = Instantiate(ice, other.transform);
            iceEffect.transform.SetParent(other.transform);
            Destroy(this.gameObject.GetComponent<Collider>());
        }
    }
}
