using UnityEngine;

public class PlayerBranchCollector : MonoBehaviour
{
    public int branchCount = 0;
    public int maxBranches = 30;

    [Header("Audio")]
    public AudioClip itemSound;
    private AudioSource audioSource;

    private UIManager uiManager;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        uiManager = FindObjectOfType<UIManager>();
        uiManager.UpdateBranchUI(branchCount, maxBranches);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Branch"))
        {
            branchCount++;
            uiManager.UpdateBranchUI(branchCount, maxBranches);

            if (itemSound && audioSource)
                audioSource.PlayOneShot(itemSound);

            other.gameObject.SetActive(false);

            if (branchCount >= maxBranches)
                GameClear();
        }
    }

    private void GameClear()
    {
        Time.timeScale = 0f;
        uiManager.ShowClearPanel();
    }
}


