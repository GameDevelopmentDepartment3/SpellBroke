using System.Collections;
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
    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
    private void Update()
    {
        burnRing.fillAmount = (float)currentBurnStack / maxBurnStack;
        Debug.Log(currentBurnStack / maxBurnStack);
        if (currentBurnStack <= 0)
            Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magic"))
        {
            var type1 = other.GetComponent<MagicDamage>().attackElement1;
            var type2 = other.GetComponent<MagicDamage>().attackElement2;
            //if(type1 == "Fire" || type2 == "Fire")
                //currentBurnStack++;
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
                currentBurnStack -= 1;
                burnDuration = 3f;
            }
        }
    }
    IEnumerator Burndamage()
    {
        var Enemy = this.gameObject.transform.parent.GetComponent<EnemyHealth>();
        for (int i = 0; i < currentBurnStack; i++)
            Enemy.TakeDamage(burnDamagePerSecond);
        Enemy.childObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(1f);
        Enemy.childObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
