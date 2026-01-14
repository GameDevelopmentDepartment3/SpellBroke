using UnityEngine;
using UnityEngine.AI;

public class KingSkeletonAttack : MonoBehaviour
{
    enum FSMStates { Idle, Chase, Attack, Summon }

    [Header("Settings")]
    public float attackRange = 5f;
    public float chaseSpeed = 5f;

    [Header("References")]
    public Animator animator;
    public NavMeshAgent agent;
    private Transform playerTransform;

    [SerializeField]
    private FSMStates currentState;

    // 공격이나 소환 애니메이션이 재생 중인지 체크하는 변수
    private bool isPerformingAction = false;

    private void Awake()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) playerTransform = playerObj.transform;

        if (agent == null) agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed;
    }

    private void Start()
    {
        currentState = FSMStates.Chase;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        // 현재 공격/소환 중이라면 거리 체크나 상태 변경을 하지 않고 리턴!
        if (isPerformingAction) return;

        if (isPerformingAction)
        {
            agent.isStopped = true;     // 정지 상태 유지
            agent.speed = 0; // 물리적인 관성 속도까지 0으로 제거
            agent.velocity = Vector3.zero;
            return;
        }
        else
        {
            agent.isStopped = false;    // 추격 상태로 전환
            agent.speed = chaseSpeed;   // 추격 속도 복원
        }

            var distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > attackRange)
        {
            currentState = FSMStates.Chase;
            ExecuteChase();
        }
        else
        {
            // 거리 안에 들어왔을 때 딱 한 번만 결정
            int value = Random.Range(0, 10);
            if (value < 7)
                ExecuteAttack();
            else
                ExecuteSummon();
        }
    }

    void ExecuteChase()
    {
        agent.isStopped = false;
        animator.SetBool("isChasing", true);
    }

    void ExecuteAttack()
    {
        isPerformingAction = true; // 동작 시작됨을 표시
        animator.SetBool("isChasing", false);
        agent.ResetPath(); // 현재 가려던 경로를 완전히 삭제하여 미끄러짐 방지
        agent.isStopped = true;
        animator.SetTrigger("isAttacking"); // 스펠링 확인: isAttacking
    }

    void ExecuteSummon()
    {
        isPerformingAction = true; // 동작 시작됨을 표시
        animator.SetBool("isChasing", false);
        agent.isStopped = true;
        animator.SetTrigger("isSummoning"); // 스펠링 확인: isSummoning
    }

    // ★ 애니메이션 이벤트에서 이 함수를 호출해야 합니다! ★
    // 애니메이션이 끝날 때 실행되어 다시 움직일 수 있게 해줍니다.
    public void OnAnimationComplete()
    {
        isPerformingAction = false;
        agent.isStopped = false; // 여기서 다시 이동 허용
        // 다시 추격 상태로 초기화
        currentState = FSMStates.Chase;
    }
}