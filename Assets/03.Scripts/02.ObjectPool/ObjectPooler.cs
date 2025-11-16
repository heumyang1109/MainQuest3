using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public static ObjectPooler Instance;

    // 
    [System.Serializable]
    public class Pool
    {
        public string tag;          // 풀을 구분하는 태그
        public GameObject prefab;   // 풀에 생성할 프리팹
        public int size;            // 초기 생성 개수
    }

    public List<Pool> pools;   // 풀 목록
    public Dictionary<string, Queue<GameObject>> poolDictionary; // 각 풀을 Queue로 관리

    private void Awake()
    {

        Instance = this;
    }

    void Start()
    {
        // 풀을 보관할 딕셔너리 생성
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // 등록된 모든 풀에 대해 초기화 수행
        foreach (Pool pool in pools)
        {
            // 객체를 담을 Queue 생성 
            Queue<GameObject> objectPool = new Queue<GameObject>();

            // 지정된 크기 만큼 프리팹 생성 
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab); // 프리팹 생성
                obj.SetActive(false);                      // 풀링이므로 비활성화
                objectPool.Enqueue(obj);                   // Queue 에 추가
            }

            // tag로 해서 딕셔너리로 저장 
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        // 해당 태그가 존재하지 않을 경우 null 반환
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        // Queue 에서 하나 꺼냄.
        GameObject obj = poolDictionary[tag].Dequeue();

        // 위치 및 회전 설정 
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        // 다시 Queue에 넣음 
        poolDictionary[tag].Enqueue(obj);

        return obj;
    }
}

