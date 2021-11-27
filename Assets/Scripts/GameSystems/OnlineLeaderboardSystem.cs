using System;
using LootLocker.Requests;
using UnityEngine;

namespace GameSystems
{
    public class OnlineLeaderboardSystem
    {
        public static readonly string CONNECTION_FAILED = "Connection failed";
        public static readonly string CONNECTION_SUCCESSFUL = "Connection successful";
        public static readonly string SCORE_SEND_FAILED = "Score send failed";
        public static readonly string SCORE_SEND_SUCCESSFUL = "Score send successful";

        public static readonly int NO_BEST_SCORE = -1;

        readonly int leaderboardID;

        int _memeberId;
        int _playerBestScore;
        int _playerLeaderboardPosition;
        string _playerName;

        public OnlineLeaderboardSystem(int leaderboardId)
        {
            leaderboardID = leaderboardId;
        }

        LootLockerLeaderboardMember[] scoreBoard { get; set; }

        public void Connect(Action<ConnectionReturnMessage> onConnected)
        {
            LootLockerSDKManager.StartGuestSession(response =>
                {
                    if (response.success)
                    {
                        _memeberId = response.player_id;

                        FetchPlayerName(name =>
                        {
                            _playerName = name;

                            FetchScores(scores =>
                            {
                                GetPlayerPosition(response =>
                                {
                                    _playerBestScore = response.score;
                                    _playerLeaderboardPosition = response.rank;

                                    ConnectedPlayer player = new ConnectedPlayer(_playerName, _memeberId,
                                        _playerBestScore,
                                        _playerLeaderboardPosition);
                                    ConnectionReturnMessage msg =
                                        new ConnectionReturnMessage(CONNECTION_SUCCESSFUL, player);
                                    onConnected?.Invoke(msg);
                                });
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

        void GetPlayerPosition(Action<LootLockerGetMemberRankResponse> onCompleted)
        {
            LootLockerSDKManager.GetMemberRank(leaderboardID.ToString(), _memeberId,
                response => { onCompleted?.Invoke(response); });
        }

        public void FetchPlayerName(Action<string> onComplete)
        {
            LootLockerSDKManager.GetPlayerName(response =>
            {
                _playerName = response.name;
                onComplete?.Invoke(response.name);
            });
        }

        public void FetchScores(Action<LootLockerLeaderboardMember[]> onCompleted)
        {
            LootLockerSDKManager.GetScoreListMain(leaderboardID, 100, 0, response =>
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
            if (name != _playerName && name != null) LootLockerSDKManager.SetPlayerName(name, null);

            LootLockerSDKManager.SubmitScore(_memeberId.ToString(), score, leaderboardID, response =>
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
    }
}