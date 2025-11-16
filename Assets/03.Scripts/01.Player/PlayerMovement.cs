using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 10f; // 이동 속도 
    public float gravity = -9.8f; // 아래로 낙하하는 중력 값 
    public float jumpHeight = 3.5f; // 플레이어의 점프 높이
    public float rotationSpeed = 10f; // 이동 방향으로 회전하는 속도

    [Header("바닥 체크 설정")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController controller;
    private PlayerInput input;
    private PlayerAnimationController animController;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        animController = GetComponent<PlayerAnimationController>();
    }

    void Update()
    {
        GroundCheck();

        // 입력값 기반 이동 벡터 생성 (x,z 방향)
        Vector3 moveInput = new Vector3(input.moveInput.x, 0f, input.moveInput.y);

        // 입력이 있으면 그 방향으로 부드럽게 회전
        if (moveInput.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        HandleJump(); // 점프 
        ApplyGravity(); // 중력 
        MoveCharacter(moveInput); // 캐릭터 이동 처리 
    }

    private void MoveCharacter(Vector3 moveInput)
    {
        // 지면 이동(x, z)
        Vector3 move = moveInput.normalized * moveSpeed;
        
        // y축에는 중력/점프 속도 적용
        move.y = velocity.y;

        controller.Move(move * Time.deltaTime);

        animController.SetRunning(moveInput.magnitude > 0.1f);
    }

    private void HandleJump()
    {
        if (isGrounded && input.jumpInput)
        {
            // 점프 속도 계산
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animController.SetJumping(true);
        }
    }

    private void ApplyGravity()
    {
        // 중력 적용
        velocity.y += gravity * Time.deltaTime;

        // 바닥에 붙으면 velocity 초기화
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
            animController.SetJumping(false);
        }
    }

    // 바닥 체크 
    private void GroundCheck()
    {
        // Raycast로 바닥과 충돌했는지 확인
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);

        // 바닥 체크 시각화 
        Debug.DrawRay(groundCheck.position, Vector3.down * groundDistance, isGrounded ? Color.green : Color.red);
    }
}
