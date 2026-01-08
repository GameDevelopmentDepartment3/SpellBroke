using UnityEngine;
using UnityEngine.InputSystem;

public class ShiftLock : MonoBehaviour
{
    public Transform cameraTransform; // 메인 카메라의 Transform을 할당하세요
    public bool isShiftLockActive = true;

    void Start()
    {
        if (isShiftLockActive)
        {
            SetShiftLock(false);
        }
    }
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.shiftKey.wasPressedThisFrame) // 왼쪽 시프트 키를 누르면 토글
        {
            SetShiftLock(!isShiftLockActive);
        }
    }

    void LateUpdate()
    {
        // 실시간으로 캐릭터를 카메라가 바라보는 수평 방향으로 회전시킵니다.
        if (isShiftLockActive && cameraTransform != null)
        {
            // 카메라의 Y축 회전값만 추출합니다.
            Vector3 cameraRotation = cameraTransform.rotation.eulerAngles;

            // 캐릭터의 회전값을 카메라의 Y축 값으로 고정합니다. (X, Z는 0으로 유지)
            transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
        }
    }

    // 마우스 커서 상태 설정 함수
    public void SetShiftLock(bool active)
    {
        isShiftLockActive = active;
        if (active)
        {
            Cursor.lockState = CursorLockMode.Locked; // 마우스를 화면 중앙에 고정
            Cursor.visible = false;                   // 마우스 커서 숨김
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;   // 고정 해제
            Cursor.visible = true;                    // 커서 표시
        }
    }
}
