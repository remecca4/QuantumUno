using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public static class GameResult
{
    public static bool humanWon  = false; 
    public static string playScene = "Easy_Mode";
}

public class EndScreenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;   // assign in Inspector

    void Start()
    {
        resultText.text = GameResult.humanWon ? "You win!" : "You lose…";
        Time.timeScale = 1f;   // un‑pause in case the game was frozen
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(GameResult.playScene);
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
