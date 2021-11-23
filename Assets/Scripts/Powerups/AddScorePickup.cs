using System;
using UnityEngine;

public class AddScorePickup : Pickup
{
    public static Action<int> OnAddScorePickup;
    
    [SerializeField] int addedScore;

    protected override void InvokeEvent()
    {
        OnAddScorePickup?.Invoke(addedScore);
    }
}