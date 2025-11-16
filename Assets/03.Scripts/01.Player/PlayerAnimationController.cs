using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("애니메이터 참조")]
    [SerializeField] private Animator animator;

    private void Reset()
    {
        // 자동으로 Animator 컴포넌트 연결 시도
        if (animator == null) 
        {
            animator = GetComponentInChildren<Animator>();
        }
           
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool("Running", isRunning);
    }

    public void SetJumping(bool isJumping)
    {
        animator.SetBool("Jumping", isJumping);
    }
}

