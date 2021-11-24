using System;
using UnityEngine;

public class BallPowerup : Pickup
{
    public static Action<BallType> OnBallPowerupPickup;

    [SerializeField] BallType powerupType;

    protected override void InvokeEvent()
    {
        OnBallPowerupPickup?.Invoke(powerupType);
    }
}