using UnityEngine;
using UnityEngine.UI;

public class HowToPlayManager : MonoBehaviour
{
    [SerializeField] private GameObject rulesPanel;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button closeButton;

    void Start()
    {
        // Set up button listeners
        howToPlayButton.onClick.AddListener(ShowRules);
        closeButton.onClick.AddListener(HideRules);
        
        // Hide panel at start
        rulesPanel.SetActive(false);
    }

    public void ShowRules()
    {
        rulesPanel.SetActive(true);
        // Optional: Pause game if in gameplay
        // Time.timeScale = 0f;
    }

    public void HideRules()
    {
        rulesPanel.SetActive(false);
        // Optional: Resume game
        // Time.timeScale = 1f;
    }
}