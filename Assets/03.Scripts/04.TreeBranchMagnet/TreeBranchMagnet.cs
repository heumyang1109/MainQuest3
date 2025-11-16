using UnityEngine;

public class TreeBranchMagnet : MonoBehaviour
{
    private Transform player;
    public float magnetRange = 8f;     // 자석 범위
    public float magnetSpeed = 10f;    // 빨려오는 속력

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // 플레이어가 일정 거리 안으로 들어오면 빨려옴
        if (distance < magnetRange)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * magnetSpeed * Time.deltaTime;
        }
    }
}
