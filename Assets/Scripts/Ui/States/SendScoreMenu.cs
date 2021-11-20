using System;
using System.Collections;
using System.Collections.Generic;
using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SendScoreMenu : UiState
{
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] TextMeshProUGUI playerCurrentScoreDisplay;
    [SerializeField] TextMeshProUGUI playerBestScoreDisplay;
    
    [Space]
    [SerializeField] int leaderboardID;
    [SerializeField] ScoreSystem scoreSystem;
    
    [Space]
    [SerializeField] LeaderboardDisplay leaderboardDisplay;

    OnlineLeaderboardSystem _onlineLeaderboardSystem;
    OnlineLeaderboardSystem.ConnectedPlayer? _connectedPlayer;

    void OnConnectedToScoreSystem(OnlineLeaderboardSystem.ConnectionReturnMessage msg)
    {
        if (msg.ConnectionMSG == OnlineLeaderboardSystem.CONNECTION_FAILED)
        {
            _connectedPlayer = null;
            return;;
        }

        _connectedPlayer = msg.Player;
        nameInputField.text = msg.Player?.Name;
        
        _onlineLeaderboardSystem.FetchScores(leaderboardDisplay.UpdateDisplay);

        if (_connectedPlayer?.BestScore == OnlineLeaderboardSystem.NO_BEST_SCORE)
        {
            playerBestScoreDisplay.text = "-";
        }
        else
        {
            playerBestScoreDisplay.text = _connectedPlayer?.BestScore.ToString();
        }
        
        
    }

    void UpdateLeaderboard(string msg) => _onlineLeaderboardSystem.FetchScores(leaderboardDisplay.UpdateDisplay); 
    
    protected override void OnStateEnter()
    {
        _onlineLeaderboardSystem = new OnlineLeaderboardSystem(leaderboardID);
        
        playerCurrentScoreDisplay.text = scoreSystem.TotalScore.ToString();
        _onlineLeaderboardSystem.Connect(OnConnectedToScoreSystem);
    }

    public void OnSendScoreBTNPress()
    {
        if (_connectedPlayer?.BestScore >= scoreSystem.TotalScore)
        {
            Debug.Log("Cant send lower score");
        }
        else
        {
            _onlineLeaderboardSystem.SendScore(nameInputField.text, scoreSystem.TotalScore, UpdateLeaderboard);
        }
    }
}
