using UnityEngine;

public class MagicTargetFollower : MonoBehaviour
{
    public Transform playerTransform; // 플레이어 본체 Transform
    public Transform magicTarget;    // 바닥에 붙을 타겟 오브젝트
    public float rayDistance = 5.0f;
    public LayerMask groundLayer;

    void Update()
    {
        // 1. 발 위치에서 아래로 레이 발사
        Ray ray = new(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, groundLayer))
        {
            // 위치 이동 (바닥 지점)
            magicTarget.position = hit.point + Vector3.up * 0.01f;

            // 2. 방향(Rotation) 설정
            // 플레이어의 정면 방향을 가져오되, 바닥의 기울기(Normal)에 맞춰 정렬합니다.
            Vector3 playerForward = playerTransform.forward;
            
            // 바닥의 법선 벡터(hit.normal)를 위쪽 방향으로 삼고, 
            // 플레이어의 앞방향을 바라보는 회전값을 계산합니다.
            Quaternion targetRotation = Quaternion.LookRotation(playerForward, hit.normal);
            
            magicTarget.rotation = targetRotation;
        }
    }
}