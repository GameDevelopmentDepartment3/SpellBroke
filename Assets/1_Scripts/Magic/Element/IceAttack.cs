using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[DefaultExecutionOrder(-100)]
public class IceAttack : MonoBehaviour
{
    [Header("원본 세팅값")]
    public int baseMaxIceStack = 5;
    public int baseCurrentIceStack = 1;
    public float baseIceDuration = 3f;
    public float baseSlowAmount = 0.15f;
    public float baseStunDuration = 0.5f;
    [Header("인게임 세팅값")]
    public int maxIceStack = 5;
    public int currentIceStack = 1;
    public float iceDuration = 3f;
    public float slowAmount = 0.15f;
    public float stunDuration = 0.5f;

    public Image IceRing;
    float enemySpeed;
    NavMeshAgent Enemy;

    private void Awake()
    {
        maxIceStack = baseMaxIceStack - UpgradeManager.Instance.minusIceStack;
        currentIceStack = baseCurrentIceStack;
        iceDuration = baseIceDuration;
        slowAmount = baseSlowAmount + UpgradeManager.Instance.plusSlowAmount;
        stunDuration = baseStunDuration;
    }

    void Start()
    {
        StartCoroutine(Ice());
        Enemy = this.gameObject.transform.parent.GetComponent<NavMeshAgent>();
        enemySpeed = Enemy.speed;
    }
    private void OnDisable()
    {
        Enemy.speed = enemySpeed;
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
