using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public event System.Action<int> OnScoreChanged;
    public event System.Action<int> OnBestScoreChanged;

    private int currentScore;
    private int bestScore;

    public int CurrentScore => currentScore;
    public int BestScore => bestScore;

    private const string BestScoreKey = "BestScore";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadBestScore();
    }

    private void OnEnable()
    {
        FruitMerge.OnFruitMerged += HandleFruitMerged;
    }

    private void OnDisable()
    {
        FruitMerge.OnFruitMerged -= HandleFruitMerged;
    }

    private void HandleFruitMerged(FruitData mergedSourceData)
    {
        int scoreToAdd = 0;

        // If mergedSourceData is tier 10 (Melon), we get a Watermelon (tier 11).
        if (mergedSourceData.nextTier != null)
        {
            scoreToAdd = mergedSourceData.nextTier.mergeScore;
        }
        // If mergedSourceData is tier 11 (Watermelon), two watermelons merged.
        else if (mergedSourceData.tier == 11)
        {
            // Watermelon merge: Base 66 + Bonus 81 = 147
            scoreToAdd = 66 + 81;
        }

        if (scoreToAdd > 0)
        {
            AddScore(scoreToAdd);
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        OnScoreChanged?.Invoke(currentScore);

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            OnBestScoreChanged?.Invoke(bestScore);
            SaveBestScore();
        }
    }

    private void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetInt(BestScoreKey, bestScore);
        PlayerPrefs.Save();
    }
}
