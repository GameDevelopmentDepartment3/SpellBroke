using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ElectricAttack : MonoBehaviour
{
    public float chainRadius = 15f;
    public int maxChainCounts = 4;
    public int currentCounts = 0;
    public List<Transform> hitEnemies = new List<Transform>();
    public GameObject electricEffectPrefab;
    void Start()
    {
        if (currentCounts < maxChainCounts)
        {
            StartCoroutine(FindNextChain());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator FindNextChain()
    {
        yield return new WaitForSeconds(0.1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, chainRadius);
        foreach (Collider collider in colliders)
        {
            Debug.LogWarning("Found collider: " + collider.name);
        }
        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy") && !hitEnemies.Contains(collider.transform))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = collider.transform;
                }
            }
        }
        if (closestEnemy == this.gameObject)
            Destroy(this.gameObject);
        if (closestEnemy != null)
        {
            hitEnemies.Add(closestEnemy);
            GameObject newElectricAttack = Instantiate(electricEffectPrefab, closestEnemy.position, Quaternion.identity);
            ElectricAttack electricAttackComponent = newElectricAttack.GetComponent<ElectricAttack>();
            newElectricAttack.GetComponent<MagicDamage>();
            electricAttackComponent.currentCounts = this.currentCounts + 1;
            electricAttackComponent.hitEnemies = new List<Transform>(hitEnemies);
            Destroy(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
}
