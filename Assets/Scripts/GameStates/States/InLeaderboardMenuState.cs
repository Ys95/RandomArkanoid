using GameSystems;
using Prefabs.UI;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameStates.States
{
    public class InLeaderboardMenuState : GameState
    {
        [SerializeField] Overlay onConnectingOverlay;

        [Space]
        [SerializeField] TMP_InputField nameInputField;
        [SerializeField] Button sendScoreBtn;
        [SerializeField] PopupMessage popupMessage;

        [Space]
        [SerializeField] GameObject playerCurrentScore;
        [SerializeField] TextMeshProUGUI playerCurrentScoreDisplay;
        [SerializeField] TextMeshProUGUI playerBestScoreDisplay;

        [Space]
        [SerializeField] int leaderboardID;
        [SerializeField] ScoreSystem scoreSystem;

        [Space]
        [SerializeField] LeaderboardDisplay leaderboardDisplay;

        readonly string _cantOverwriteScore = "Can't send score lower than your best score.";
        readonly string _scoreSend = "Score send successfully.";

        OnlineLeaderboardSystem.ConnectedPlayer? _connectedPlayer;
        OnlineLeaderboardSystem _onlineLeaderboardSystem;

        bool PlayerEligibleToSendScore => _connectedPlayer?.BestScore < scoreSystem.TotalScore;

        void OnConnectedToScoreSystem(OnlineLeaderboardSystem.ConnectionReturnMessage msg)
        {
            bool connectionSucceeded = false;

            if (msg.ConnectionMSG == OnlineLeaderboardSystem.CONNECTION_FAILED)
            {
                _connectedPlayer = null;
                connectionSucceeded = false;
            }
            else
            {
                connectionSucceeded = true;
            }

            if (connectionSucceeded == false)
            {
                onConnectingOverlay.DisplayFailedOverlay();
                return;
            }

            _connectedPlayer = msg.Player;
            nameInputField.text = msg.Player?.Name;

            _onlineLeaderboardSystem.FetchScores(response =>
            {
                if (response == null) return;
                leaderboardDisplay.UpdateDisplay(response);
            });


            if (_connectedPlayer?.BestScore == OnlineLeaderboardSystem.NO_BEST_SCORE)
                playerBestScoreDisplay.text = "-";
            else
                playerBestScoreDisplay.text = _connectedPlayer?.BestScore.ToString();

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
            playerCurrentScore.SetActive(false);
        }

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
            _onlineLeaderboardSystem.FetchScores(response =>
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
            playerCurrentScore.SetActive(true);
            popupMessage.Hide();
        }
    }
}