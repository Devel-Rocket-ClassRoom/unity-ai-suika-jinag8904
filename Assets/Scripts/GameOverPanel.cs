using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestText;

    void OnEnable()
    {
        if (ScoreManager.Instance == null)
            return;

        scoreText.text = $"SCORE  {ScoreManager.Instance.CurrentScore}";
        bestText.text = $"BEST  {ScoreManager.Instance.BestScore}";
    }
}
