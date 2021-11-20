using System;
using System.Collections;
using System.Collections.Generic;
using LootLocker.Requests;
using UnityEngine;

[Serializable]
public class LeaderboardDisplay
{
    [SerializeField] ScoreText[] displaySlots;

    public void UpdateDisplay(LootLockerLeaderboardMember[] scores)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            displaySlots[i].PlayerName.text = scores[i].player.name;
            displaySlots[i].Score.text = scores[i].score.ToString();
        }
        FillEmptyDisplaySlots(scores.Length);
    }

    void FillEmptyDisplaySlots(int scoresAmount)
    {
        for (int j = scoresAmount; j < displaySlots.Length; j++)
        {
            displaySlots[j].PlayerName.text = "-";
            displaySlots[j].Score.text = "-";
        }
    }
}
