using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 35f;
    public float rotationSpeed = 10f;
    
    [Header("Jump Settings")]
    public float jumpHeight = 8f;   // 점프 높이
    public float gravity = -60f;      // 중력 세기 (기본값보다 조금 강하게 설정하면 묵직함)
    
    [Header("References")]
    public Transform cameraTransform;

    private CharacterController _controller;
    private PlayerControls _controls;
    private Vector2 _moveInput;
    private Vector3 _velocity; // Y축 속도 저장용
    private bool _isGrounded;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _controls = new PlayerControls();

        // [점프 이벤트 등록] 점프 버튼을 누른 '순간' 실행
        _controls.Player.Jump.performed += ctx => OnJump();
    }

    void OnEnable() => _controls.Enable();
    void OnDisable() => _controls.Disable();

    void Update()
    {
        // 1. 땅 체크
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // 땅에 붙어있도록 작은 힘 유지
        }

        // 2. 이동 처리
        _moveInput = _controls.Player.Move.ReadValue<Vector2>();
        
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * _moveInput.y + right * _moveInput.x).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            
            _controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        // 3. 중력 적용
        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    // 점프 함수
    private void OnJump()
    {
        if (_isGrounded)
        {
            // 물리 공식: v = sqrt(h * -2 * g)
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}