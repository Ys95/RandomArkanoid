using System;
using System.Collections;
using System.Collections.Generic;
using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 
public class InLeaderboardMenuState : GameState
{
    [SerializeField] Overlay onConnectingOverlay;
    
    [Space]
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] Button sendScoreBtn;
    [SerializeField] PopupMessage popupMessage;
    
    [Space]
    [SerializeField] TextMeshProUGUI playerCurrentScoreDisplay;
    [SerializeField] TextMeshProUGUI playerBestScoreDisplay;
    
    [Space]
    [SerializeField] int leaderboardID;
    [SerializeField] ScoreSystem scoreSystem;
    
    [Space]
    [SerializeField] LeaderboardDisplay leaderboardDisplay;
    
    OnlineLeaderboardSystem _onlineLeaderboardSystem;
    OnlineLeaderboardSystem.ConnectedPlayer? _connectedPlayer;

    readonly string _cantOverwriteScore = "Can't send score lower than your best score.";
    readonly string _scoreSend = "Score send successfully.";

    void OnConnectedToScoreSystem(OnlineLeaderboardSystem.ConnectionReturnMessage msg)
    {
        if (msg.ConnectionMSG == OnlineLeaderboardSystem.CONNECTION_FAILED)
        {
            _connectedPlayer = null;
            onConnectingOverlay.DisplayIdleOverlay(false);
            onConnectingOverlay.DisplayFailedOverlay(true);
            return;
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

        onConnectingOverlay.TurnOff();
    }
    
    protected override void OnStateEnter()
    {
        onConnectingOverlay.TurnOn();
        
        _onlineLeaderboardSystem = new OnlineLeaderboardSystem(leaderboardID);
        
        playerCurrentScoreDisplay.text = scoreSystem.TotalScore.ToString();
        _onlineLeaderboardSystem.Connect(OnConnectedToScoreSystem);
    }

    public void OpenLeaderboardsOnly()
    {
        Open();
        nameInputField.interactable = false;
        sendScoreBtn.gameObject.SetActive(false);
    }
    
    bool PlayerEligibleToSendScore => _connectedPlayer?.BestScore < scoreSystem.TotalScore;
    public void OnSendScoreBTNPress()
    {
        if (PlayerEligibleToSendScore)
        {
            onConnectingOverlay.TurnOn();
            _onlineLeaderboardSystem.SendScore(nameInputField.text, scoreSystem.TotalScore, OnScoreSend);
            return;
        }
        popupMessage.Show(_cantOverwriteScore);
    }
    
    void OnScoreSend(string msg)
    {
        _onlineLeaderboardSystem.FetchScores((response) =>
        {
            leaderboardDisplay.UpdateDisplay(response);
            onConnectingOverlay.TurnOff();
            playerBestScoreDisplay.text = playerCurrentScoreDisplay.text;
            popupMessage.Show(_scoreSend);
        });
    }
    
    protected override void OnStateExit()
    {
        nameInputField.interactable = true;
        sendScoreBtn.gameObject.SetActive(true);
        popupMessage.Hide();
    }
}
