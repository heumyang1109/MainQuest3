using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI branchText;
    public GameObject clearPanel;
    public Button restartButton; // ClearPanel 안 버튼
    public Button quitButton;    // ClearPanel 안 버튼

    private void Start()
    {
        if (clearPanel != null)
            clearPanel.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    public void UpdateBranchUI(int count, int max)
    {
        if (branchText != null)
            branchText.text = $"Branches: {count}/{max}";
    }

    public void ShowClearPanel()
    {
        if (clearPanel != null)
            clearPanel.SetActive(true);
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
