using System;
using UnityEngine;

public class BallPowerUp : PowerUp
{
    [SerializeField] BallType powerupType;
    public static Action<BallType> OnBallPowerupPickup;

    protected override void InvokeEvent() => OnBallPowerupPickup?.Invoke(powerupType);
}
