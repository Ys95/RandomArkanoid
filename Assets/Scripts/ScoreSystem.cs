using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    int _score;

    void OnEnable()
    {
        
    }

    public void AddScore(int amount)
    {
        _score += amount;
    }
}