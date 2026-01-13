using UnityEngine;

public class KingSkeletonAttack : MonoBehaviour
{
    enum fSMStates
    {
        Idle,
        Chase,
        Attack,
        Summon
    }
    public Vector3 player;
    public Animator animator;
    private fSMStates currenState;
    bool isAttacking = false;
    bool isSummoning = false;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    private void Update()
    {
        switch (currenState)
        {
            case fSMStates.Idle:
                // Idle behavior
                break;
            case fSMStates.Chase:
                // Chase behavior
                break;
            case fSMStates.Attack:
                // Attack behavior
                break;
            case fSMStates.Summon:
                // Summon behavior
                break;
            default:
                break;
        }
    }
    public void ExcuteIdlePattern()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player);
        if (distanceToPlayer < 10f)
        {
            currenState = fSMStates.Chase;
            animator.SetBool("isChasing", true);
        }
    }
    public void ExcuteAttackPattern()
    {
        if (isAttacking) return;
        isAttacking = true;
        animator.SetTrigger("isAttacking");
    }
    public void ExcuteSummonPattern()
    {
        if (isSummoning) return;
        isSummoning = true;
        animator.SetTrigger("isSummoning");
    }
    public void OnAttackStart()
    {
        // Logic for starting the attack (e.g., enabling hitboxes)
        Debug.Log("Attack started.");
    }
    public void OnAttackEnd()
    {
        // Logic for ending the attack (e.g., disabling hitboxes)
        Debug.Log("Attack ended.");
        isAttacking = false;
    }
    public void OnSummonStart()
    {
        // Logic for starting the summon
        Debug.Log("Summon started.");
    }
    public void OnSummonEnd()
    {
        // Logic for ending the summon
        Debug.Log("Summon ended.");
        isSummoning = false;
    }
}
