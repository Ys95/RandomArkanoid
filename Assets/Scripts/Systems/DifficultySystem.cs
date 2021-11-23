using UnityEngine;

public class DifficultySystem : MonoBehaviour
{
    [SerializeField] int startingDifficulty;
    
    public int CurrentDifficultyLevel { get; private set; }

    public void ResetDifficulty()
    {
        CurrentDifficultyLevel = startingDifficulty;
    }

    public void IncreaseDifficultyLevel()
    {
        CurrentDifficultyLevel++;
    }
}