using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BetweenLevelsTransitionState : GameState
{
    [SerializeField] GameState inGameState;
    
    [Space]
    [SerializeField] DifficultySystem difficultySystem;
    [SerializeField] ScoreSystem scoreSystem;

    [Space]
    [SerializeField] TextMeshProUGUI levelScoreDisplay;
    [SerializeField] TextMeshProUGUI totalScoreDisplay;

    [Header("Sum score effect")]
    [SerializeField] float timeBetweenTicks;
    IEnumerator _effectCoroutine;
    
    delegate void AnyButtonPressAction();
    AnyButtonPressAction _onAnyButtonPressAction;
    
    public override void HandleAnyKeyPress(InputAction.CallbackContext context) 
    {
        base.HandleAnyKeyPress(context);
        _onAnyButtonPressAction();
    }
    
    protected override void OnStateEnter()
    {
        GameManager.Instance.StartNewLevel();
        
        _effectCoroutine = SumScoreEffect();
        StartCoroutine(_effectCoroutine);

        _onAnyButtonPressAction = SkipEffect;
            
        levelScoreDisplay.text = scoreSystem.PreviousLevelScore.ToString();
        totalScoreDisplay.text = scoreSystem.PreviousTotalScore.ToString();
    }

    void SkipEffect()
    {
        StopCoroutine(_effectCoroutine);

        levelScoreDisplay.text = "0";
        totalScoreDisplay.text = scoreSystem.TotalScore.ToString();
        
        _onAnyButtonPressAction = () => GameStateController.GoToNewState(inGameState);
    }

    IEnumerator SumScoreEffect()
    {
        int levelScore = scoreSystem.PreviousLevelScore;
        int totalScore = scoreSystem.PreviousTotalScore;
        
        levelScoreDisplay.text = levelScore.ToString();
        totalScoreDisplay.text = totalScore.ToString();

        while (levelScore > 0)
        {
            totalScore++;
            levelScore--;
            
            levelScoreDisplay.text = levelScore.ToString();
            totalScoreDisplay.text = totalScore.ToString();

            yield return new WaitForSeconds(timeBetweenTicks);
        }
        _onAnyButtonPressAction = () => GameStateController.GoToNewState(inGameState);
    }
    
}
