using GameSystems;
using TMPro;
using UnityEngine;

namespace GameStates.States
{
    public class GameOverState : GameState
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
}