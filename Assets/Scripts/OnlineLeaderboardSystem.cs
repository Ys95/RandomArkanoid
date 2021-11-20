using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using LootLocker.Requests;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

public class OnlineLeaderboardSystem
{
    public struct ConnectionReturnMessage
    {
        public readonly ConnectedPlayer? Player;
        public readonly string ConnectionMSG;
        public ConnectionReturnMessage(string connectionMsg, ConnectedPlayer? player)
        {
            ConnectionMSG = connectionMsg;
            Player = player;
        }
    }

    public readonly struct ConnectedPlayer
    {
        public readonly string Name;
        public readonly int ID;
        public readonly int BestScore;
        public readonly int Rank;

        public ConnectedPlayer(string name, int id, int bestScore, int rank)
        {
            Name = name;
            ID = id;
            BestScore = bestScore;
            Rank = rank;
        }
    }
    
    public static readonly string CONNECTION_FAILED = "Connection failed";
    public static readonly string CONNECTION_SUCCESSFUL= "Connection successful";
    
    public static readonly string SCORE_SEND_FAILED = "Score send failed";
    public static readonly string SCORE_SEND_SUCCESSFUL= "Score send successful";

    public static readonly int NO_BEST_SCORE = -1;
    
    readonly int leaderboardID;

    int _memeberId;
    string _playerName;
    int _playerBestScore;
    int _playerLeaderboardPosition;

    
    LootLockerLeaderboardMember[] scoreBoard { get; set; }
    
    public void Connect(Action<ConnectionReturnMessage> onConnected)
    {
        LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (response.success)
                {
                    _memeberId = response.player_id;

                    FetchPlayerName((name) =>
                    {
                        _playerName = name;
                        
                        FetchScores((scores) =>
                        {
                            GetPlayerPosition(scores, out _playerLeaderboardPosition, out _playerBestScore);
                            
                            ConnectedPlayer player = new ConnectedPlayer(_playerName, _memeberId, _playerBestScore, _playerLeaderboardPosition);
                            ConnectionReturnMessage msg = new ConnectionReturnMessage(CONNECTION_SUCCESSFUL, player);
                            onConnected?.Invoke(msg);
                            
                        });
                    });
                }
                else
                {
                    ConnectionReturnMessage msg = new ConnectionReturnMessage(CONNECTION_FAILED, null);
                    
                    onConnected?.Invoke(msg);
                    
                    Debug.Log(CONNECTION_FAILED);
                }
            }
        );
    }
    

    void GetPlayerPosition(LootLockerLeaderboardMember[] scores, out int rank, out int score)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i].player.id == _memeberId)
            {
                score = scores[i].score;
                rank = scores[i].rank;
                return;
            }
        }
        rank = NO_BEST_SCORE;
        score = NO_BEST_SCORE;
    }
    
    public void FetchPlayerName(Action<string> onComplete)
    {
        LootLockerSDKManager.GetPlayerName((response) =>
        {
            _playerName = response.name;
            onComplete?.Invoke(response.name);
        });
    }
    
    public void FetchScores(Action<LootLockerLeaderboardMember[]> onCompleted)
    {
        LootLockerSDKManager.GetScoreListMain(809, 6, 0, (response) =>
        {
            if (response.success)
            {
                scoreBoard = response.items;
                onCompleted?.Invoke(response.items);
                Debug.Log("Score fetch successful");
            } 
            else 
            {
                onCompleted?.Invoke(null);
                Debug.Log("Score fetch failed: " + response.Error);
            }
        });
    }

    public void SendScore(string name, int score, Action<string> onSend)
    {
        if (name != _playerName && name != null)
        {
            LootLockerSDKManager.SetPlayerName(name, null);
        }
        
        LootLockerSDKManager.SubmitScore(_memeberId.ToString(), score, leaderboardID, (response) =>
        {
            if (response.success)
            {
                onSend?.Invoke(SCORE_SEND_SUCCESSFUL);
                Debug.Log("Score send successfully");
            }
            else
            {
                onSend?.Invoke(SCORE_SEND_FAILED);
                Debug.Log("Score send failed");
            }
        });
    }

    public OnlineLeaderboardSystem(int leaderboardId)
    {
        leaderboardID = leaderboardId;
    }
}