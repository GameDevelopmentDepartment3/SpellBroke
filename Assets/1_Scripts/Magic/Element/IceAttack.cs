using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class IceAttack : MonoBehaviour
{
    public int maxIceStack = 3;
    public int currentIceStack = 0;
    public float iceDuration = 3f;
    public float slowAmount = 0.15f;
    public float stunDuration = 0.5f;
    public Image IceRing;
    float enemySpeed;
    NavMeshAgent Enemy;

    void Start()
    {
        StartCoroutine(Ice());
        Enemy = this.gameObject.transform.parent.GetComponent<NavMeshAgent>();
        enemySpeed = Enemy.speed;
    }
    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
    IEnumerator Ice()
    {
        while (currentIceStack > 0)
        {
            yield return new WaitForSeconds(1f);
            iceDuration -= 1f;
            if (iceDuration <= 0f)
            {
                currentIceStack = 0;
                iceDuration = 3;
            }
        }
    }
    void Update()
    {
        IceRing.fillAmount = (float)currentIceStack / maxIceStack;
        if(currentIceStack >= maxIceStack)
            Enemy.speed = 0;
        else
            Enemy.speed = enemySpeed * (1 - slowAmount * currentIceStack);
        if (currentIceStack <= 0)
            Destroy(this.gameObject);
    }
}
