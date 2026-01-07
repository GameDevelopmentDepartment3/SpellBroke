// using UnityEngine;
// using UnityEngine.AI;

// [RequireComponent(typeof(NavMeshAgent))]
// public class MonsterAi : MonoBehaviour
// {
//     private Transform target;
//     private NavMeshAgent agent;

//     void Start()
//     {
//         agent = GetComponent<NavMeshAgent>();
        
//         GameObject playerObj = GameObject.FindWithTag("Player");
//         if (playerObj != null)
//         {
//             target = playerObj.transform;
//         }
        
//     }

//     void Update()
//     {
//         if (target != null)
//         {
//             agent.SetDestination(target.position);
//         }
//     }
    
// }

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterAi : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;

    [Header("거리 설정")]
    public float stopDistance = 3.0f; // 멈출 거리

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        // --- [중요] 컴포넌트 설정 최적화 ---
        agent.stoppingDistance = stopDistance;
        agent.acceleration = 999f;      // 가속도를 극단적으로 높여 즉각 반응하게 함
        agent.angularSpeed = 1000f;    // 회전 속도를 매우 높임
        agent.autoBraking = true;      // 목적지 근처에서 자동으로 속도 줄임

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null) target = playerObj.transform;
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= stopDistance)
        {
            // [해결책] 거리 안에 들어오면 속도를 강제로 0으로 만들고 경로를 비움
            agent.isStopped = true;
            agent.velocity = Vector3.zero; 
            
            // 멈춰있을 때 플레이어를 계속 바라보게 함 (선택 사항)
            LookAtTarget();
        }
        else
        {
            // 거리가 멀어지면 다시 추격
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
    }

    void LookAtTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0; // 위아래로 기울어지지 않게 함
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}