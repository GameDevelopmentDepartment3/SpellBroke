using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FireAttack : MonoBehaviour
{
    public int maxBurnStack = 5;
    public int currentBurnStack = 0;
    public float burnDamagePerSecond = 5f;
    public float burnDuration = 3f;
    public float explosionDamage = 50f;
    public GameObject fireExplosionPrefab;
    public Image burnRing;
    private void Start()
    {
        StartCoroutine(Burn());
    }

    private void Update()
    {
        burnRing.fillAmount = currentBurnStack / maxBurnStack;
        if (currentBurnStack <= 0)
            Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            currentBurnStack++;
        }
    }
    IEnumerator Burn()
    {
        while (currentBurnStack > 0)
        {
            StartCoroutine(Burndamage());
            yield return new WaitForSeconds(1f);
            burnDuration -= 1f;
            if (burnDuration <= 0f)
            {
                currentBurnStack = 0;
            }
        }
    }
    IEnumerator Burndamage()
    {
        var Enemy = transform.parent.GetComponent<EnemyHealth>();
        for (int i = 0; i < currentBurnStack; i++)
            Enemy.TakeDamage(burnDamagePerSecond);
        Enemy.childObject.GetComponent<Material>().color = Color.red;
        yield return new WaitForSeconds(1f);
        Enemy.childObject.GetComponent<Material>().color = Color.white;
    }
}
