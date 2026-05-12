using UnityEngine;
using TMPro;

public class ScoreUI_Canvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    private void Start()
    {
        if (ScoreManager.Instance != null)
        {
            UpdateCurrentScore(ScoreManager.Instance.CurrentScore);
            UpdateBestScore(ScoreManager.Instance.BestScore);
            
            ScoreManager.Instance.OnScoreChanged += UpdateCurrentScore;
            ScoreManager.Instance.OnBestScoreChanged += UpdateBestScore;
        }
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateCurrentScore;
            ScoreManager.Instance.OnBestScoreChanged -= UpdateBestScore;
        }
    }

    private void UpdateCurrentScore(int score)
    {
        if (currentScoreText != null)
            currentScoreText.text = $"SCORE: {score}";
    }

    private void UpdateBestScore(int score)
    {
        if (bestScoreText != null)
            bestScoreText.text = $"BEST: {score}";
    }
}
