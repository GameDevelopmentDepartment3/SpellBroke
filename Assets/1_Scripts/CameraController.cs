using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform playerTarget;
    
    [Header("Settings")]
    public float mouseSensitivity = 0.1f;
    public float distanceFromPlayer = 5f;
    public float minDistance = 1.0f; // 장애물 때문에 가까워질 때의 최소 거리
    public float smoothTime = 0.12f;
    
    [Header("Collision Settings")]
    public LayerMask collisionLayers; // 카메라가 충돌할 레이어 (Ground, Wall 등)
    public float cameraRadius = 0.2f; // 카메라 주위의 가상의 구체 크기 (벽 뚫림 방지)

    private PlayerControls _controls;
    private Vector2 _currentLookVelocity;
    private Vector2 _smoothLookInput;
    private float _rotationX;
    private float _rotationY;
    private float _currentDistance; // 현재 계산된 카메라 거리

    void Awake() => _controls = new PlayerControls();
    void OnEnable() => _controls.Enable();
    void OnDisable() => _controls.Disable();

    void Start()
    {
        _currentDistance = distanceFromPlayer;
        // 마우스 커서 고정
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (playerTarget == null) return;

        // 1. 입력 및 회전 계산
        Vector2 rawInput = _controls.Player.Look.ReadValue<Vector2>();
        _smoothLookInput = Vector2.SmoothDamp(_smoothLookInput, rawInput, ref _currentLookVelocity, smoothTime);

        _rotationY += _smoothLookInput.x * mouseSensitivity;
        _rotationX -= _smoothLookInput.y * mouseSensitivity;
        _rotationX = Mathf.Clamp(_rotationX, -40f, 80f);

        Quaternion rotation = Quaternion.Euler(_rotationX, _rotationY, 0);

        // 2. 카메라 충돌 처리 (Collision Check)
        // 플레이어에서 카메라 방향으로 레이를 쏴서 장애물이 있는지 확인합니다.
        Vector3 defaultPosition = playerTarget.position - (rotation * Vector3.forward * distanceFromPlayer);
        Vector3 direction = (defaultPosition - playerTarget.position).normalized;
        
        RaycastHit hit;
        // SphereCast를 쓰면 점(Ray)이 아니라 구체(Sphere)를 쏴서 훨씬 정확하게 벽을 감지합니다.
        if (Physics.SphereCast(playerTarget.position, cameraRadius, direction, out hit, distanceFromPlayer, collisionLayers))
        {
            // 장애물이 있다면, 장애물 지점까지만 거리를 제한합니다.
            _currentDistance = Mathf.Clamp(hit.distance, minDistance, distanceFromPlayer);
        }
        else
        {
            // 장애물이 없다면 원래 거리를 유지합니다.
            _currentDistance = distanceFromPlayer;
        }

        // 3. 최종 위치 적용
        transform.rotation = rotation;
        transform.position = playerTarget.position - (rotation * Vector3.forward * _currentDistance);
    }
}