using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public Vector2 moveInput;  // 이동 입력 (x, y)
    [HideInInspector] public bool jumpInput;     // 점프 입력

    void Update()
    {
        // 이동키 입력 (WASD)
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // 점프 입력 (스페이스바)
        jumpInput = Input.GetKeyDown(KeyCode.Space);
    }
}
