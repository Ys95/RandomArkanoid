using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreDisplay;
    
    int _totalScore;
    int _levelScore;

    void OnEnable()
    {
        _levelScore = 0;
        UpdateScoreDisplay();
        PowerUp.OnPowerupPickup += PowerupPickupScore;
    }

    void PowerupPickupScore() => AddLevelScore(10);

    public void AddLevelScore(int amount)
    {
        _levelScore += amount;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        scoreDisplay.text = _levelScore.ToString();
    }

    void OnDisable()
    {
        PowerUp.OnPowerupPickup -= PowerupPickupScore;
    }
}