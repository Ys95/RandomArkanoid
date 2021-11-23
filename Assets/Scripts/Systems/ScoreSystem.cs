using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] int _totalScore;
    
    public int TotalScore => _totalScore;
    public int LevelScore { get; private set; }
    public int PreviousLevelScore { get; private set; }
    public int PreviousTotalScore { get; private set; }

    void OnEnable()
    {
        LevelScore = 0;
        UpdateScoreDisplay();
        AddScorePickup.OnAddScorePickup += AddLevelScore;
    }

    void OnDisable()
    {
        AddScorePickup.OnAddScorePickup -= AddLevelScore;
    }

    void AddLevelScore(int amount)
    {
        LevelScore += amount;
        if (LevelScore < 0) LevelScore = 0;
        UpdateScoreDisplay();
    }

    public void AddBrickScore(Vector2 pos, int amount)
    {
        AddLevelScore(amount);
        UpdateScoreDisplay();
    }

    public void ResetAllScore()
    {
        LevelScore = 0;
        _totalScore = 0;
        UpdateScoreDisplay();
    }

    public void SumScores()
    {
        PreviousTotalScore = _totalScore;
        _totalScore += LevelScore;

        PreviousLevelScore = LevelScore;
        LevelScore = 0;
    }

    public void UpdateScoreDisplay()
    {
        scoreDisplay.text = LevelScore.ToString();
    }
}