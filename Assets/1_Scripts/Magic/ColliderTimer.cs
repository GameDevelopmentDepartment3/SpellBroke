using UnityEngine;
using System.Collections;

public class ColliderTimer : MonoBehaviour
{
    public float delayTime = 0.5f; // 활성화될 때까지 기다릴 시간
    private Collider myCollider;

    void Awake()
    {
        // 1. 컴포넌트를 미리 가져오고 처음에 꺼둡니다.
        myCollider = GetComponent<Collider>();
        if (myCollider != null)
        {
            myCollider.enabled = false;
        }
    }

    void OnEnable()
    {
        // 2. 오브젝트가 활성화(Instantiate 포함)될 때마다 타이머 시작
        if (myCollider != null)
        {
            StopAllCoroutines(); // 혹시 모를 중복 실행 방지
            StartCoroutine(EnableColliderAfterDelay());
        }
    }

    IEnumerator EnableColliderAfterDelay()
    {
        // 3. 지정된 시간만큼 대기
        yield return new WaitForSeconds(delayTime);

        // 4. 콜라이더 활성화
        myCollider.enabled = true;
        // Debug.Log("콜라이더가 활성화되었습니다!");
    }
}