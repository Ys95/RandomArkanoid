using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesSystem : MonoBehaviour
{
    public int LivesLeft => _livesLeft;
    
    [SerializeField] int initialLivesAmount;
    
    int _livesLeft;

    public void AddChance() => _livesLeft++;

    public void LoseLife()
    {
        _livesLeft--;
        
        if (_livesLeft >= 0) GameManager.Instance.LoseLife();
        else GameManager.Instance.GameOver();
    }
    
    public void ResetChances()
    {
        _livesLeft = initialLivesAmount;
    }
}

