using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverMenu : UiState
{
    [Space]
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] ScoreSystem scoreSystem;

    
    protected override void OnStateEnter()
    {
        scoreSystem.SumScores();
        scoreDisplay.text = scoreSystem.TotalScore.ToString();
    }
}
