using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // TextMeshPro를 사용할 경우

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI textMesh; // TMP 사용 시
    
    public float moveSpeed = 1.0f;
    public float disappearSpeed = 3.0f;
    public float destroyTime = 1.0f;
    private Color textColor;

    public void Setup(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
        textColor = textMesh.color;
        StartCoroutine(PunchScaleAnimation());
        Destroy(gameObject, destroyTime); // 일정 시간 후 자동 삭제
    }

    IEnumerator PunchScaleAnimation()
    {
        float duration = 0.2f; // 애니메이션 속도
        Vector3 startScale = Vector3.zero;
        Vector3 maxScale = new Vector3(0.1f, 0.1f, 0.1f); // 뻥튀기될 크기
        Vector3 finalScale = new Vector3(0.05f, 0.05f, 0.05f); // 원래 크기

        float elapsed = 0f;

        // 1. 순식간에 커지기
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, maxScale, elapsed / duration);
            yield return null;
        }

        // 2. 부드럽게 원래 크기로 돌아오기
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(maxScale, finalScale, elapsed / duration);
            yield return null;
        }
    }

    void Update()
    {
        // 1. 위로 이동
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // 2. 서서히 투명해짐 (Alpha값 조절)
        textColor.a -= disappearSpeed * Time.deltaTime;
        // textMesh.color = textColor;

        // 카메라가 바라보는 방향을 똑같이 바라보게 함
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}