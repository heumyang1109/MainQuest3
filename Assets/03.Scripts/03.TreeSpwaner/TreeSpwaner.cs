using UnityEngine;



public class TreeSpawner : MonoBehaviour
{
    [Header("Reference")]
    public ObjectPooler pooler;

    [Header("Spawn Settings")]
    public float spawnInterval = 2.0f; // 2초마다 스폰

    [Header("Spawn Range")]
    public float minX = -40f;
    public float maxX = 40f;
    public float minZ = -40f;
    public float maxZ = 40f;

    [Header("Raycast Settings")]
    public float rayStartHeight = 50f; // 위에서 Ray를 쏠 시작 높이
    public LayerMask groundLayer; // 지형 레이어만 체크

    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomTree), 0f, spawnInterval);
    }

    void SpawnRandomTree()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        // 위에서 아래로 RayCast
        Ray ray = new Ray(new Vector3(randomX, rayStartHeight, randomZ), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Vector3 spawnPos = hit.point;
            pooler.SpawnFromPool("TreeBranch", spawnPos, Quaternion.identity);
        }

    }
}

