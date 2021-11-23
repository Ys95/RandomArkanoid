using TMPro;
using UnityEngine;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI playerPosition;
    [SerializeField] TextMeshProUGUI playerScore;
    
    int _index;

    public void SetEntryIndex(int index)
    {
        _index = index;
        playerPosition.text = index.ToString();
    }

    public void UpdateEntry(string name, int score)
    {
        playerName.text = name;
        playerScore.text = score.ToString();
    }

    public void FillWithDefaultValue()
    {
        playerName.text = "-";
        playerPosition.text = _index.ToString();
        playerScore.text = "-";
    }
}