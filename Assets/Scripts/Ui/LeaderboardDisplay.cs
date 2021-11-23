using System;
using LootLocker.Requests;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public class LeaderboardDisplay : MonoBehaviour
{
    [SerializeField] GameObject slotPrefab;
    
    [Space]
    [SerializeField] int maxEntries;
    [SerializeField] Transform entriesParent;
    [SerializeField] LeaderboardEntry[] displayedEntries;

    [ContextMenu("Create display")]
    void CreateDisplay()
    {
        displayedEntries = new LeaderboardEntry[maxEntries];
        for (var i = 0; i < maxEntries; i++)
        {
            displayedEntries[i] = PrefabUtility.InstantiatePrefab(slotPrefab, entriesParent)
                .GetComponent<LeaderboardEntry>();
            displayedEntries[i].SetEntryIndex(i + 1);
            displayedEntries[i].FillWithDefaultValue();
        }
    }

    public void UpdateDisplay(LootLockerLeaderboardMember[] scores)
    {
        for (var i = 0; i < scores.Length; i++)
        {
            if (i >= displayedEntries.Length) return;
            displayedEntries[i].UpdateEntry(scores[i].player.name, scores[i].score);
        }
    }
}