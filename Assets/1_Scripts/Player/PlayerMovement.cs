using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 15f;
    public float rotationSpeed = 10f;
    
    [Header("Jump Settings")]
    public float jumpHeight = 3f;   
    public float gravity = -20f;      
    public float groundCheckDistance = 0.3f; // 발밑 감지 거리

    [Header("References")]
    public Transform cameraTransform;

    private CharacterController _controller;
    private PlayerControls _controls;
    private Vector2 _moveInput;
    private Vector3 _velocity; 
    private bool _isGrounded;

    private Animator _anim;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _controls = new PlayerControls();
        _anim = GetComponent<Animator>();

        _controls.Player.Jump.performed += ctx => OnJump();
    }

    void OnEnable() => _controls.Enable();
    void OnDisable() => _controls.Disable();

    void Update()
    {
        // 1. 피직스 매터리얼 기반 땅 체크
        _isGrounded = CheckGroundWithMaterial();
        
        if (_isGrounded && _velocity.y < 0)
        {
            if(_anim.GetBool("isJump")) _anim.SetBool("isJump", false);
            _velocity.y = -2f; 
        }

        // 2. 이동 입력 처리
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
            UpdateAnimation(true);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            _controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            UpdateAnimation(false);
        }

        // 3. 중력 적용
        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    // [핵심 함수] 피직스 매터리얼 감지 로직
    private bool CheckGroundWithMaterial()
    {
        RaycastHit hit;
        // 캐릭터의 중심 아래방향으로 Ray를 발사
        // CharacterController의 높이 절반 지점부터 아래로 쏩니다.
        Vector3 rayStart = transform.position + Vector3.up * 0.1f; 

        if (Physics.Raycast(rayStart, Vector3.down, out hit, groundCheckDistance))
        {
            // 충돌한 물체의 Collider에 Physics Material이 있는지 확인
            if (hit.collider.sharedMaterial != null)
            {
                // 매터리얼 이름에 "ground"가 포함되어 있는지 확인 (대소문자 구분 주의)
                if (hit.collider.sharedMaterial.name.Contains("ground"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void UpdateAnimation(bool isMoving)
    {
        _anim.SetBool("isRun", isMoving);
        _anim.SetBool("isIdle", !isMoving);
    }

    private void OnJump()
    {
        if (!_isGrounded) return;
        _anim.SetBool("isJump", true);
        _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    // 에디터 뷰에서 감지 선 시각화
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.down * groundCheckDistance);
    }
}