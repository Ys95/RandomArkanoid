using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int TotalScore => _totalScore;
    public int LevelScore => _levelScore;
    public int PreviousLevelScore => _previousLevelScore;
    public int PreviousTotalScore => _previousTotalScore;
    
    [SerializeField] TextMeshProUGUI scoreDisplay;
    
    int _totalScore;
    int _levelScore;
    int _previousLevelScore;
    int _previousTotalScore;

    void OnEnable()
    {
        _levelScore = 0;
        UpdateScoreDisplay();
        PowerUp.OnPowerupPickup += PowerupPickupScore;
    }

    void AddLevelScore(int amount) => _levelScore += amount;
    
    void PowerupPickupScore() => AddLevelScore(10);

    public void AddBrickScore(Vector2 pos, int amount)
    {
        AddLevelScore(amount);
        UpdateScoreDisplay();
    }
    
    public void ResetAllScore()
    {
        _levelScore = 0;
        _totalScore = 0;
        UpdateScoreDisplay();
    }
    
    public void SumScores()
    {
        _previousTotalScore = _totalScore;
        _totalScore += _levelScore;
        
        _previousLevelScore = _levelScore;
        _levelScore = 0;
    }

    public void UpdateScoreDisplay()
    {
        scoreDisplay.text = _levelScore.ToString();
    }

    void OnDisable()
    {
        PowerUp.OnPowerupPickup -= PowerupPickupScore;
    }
}