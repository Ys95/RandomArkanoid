using System;
using UnityEngine;

public class RacketPowerup : PowerUp
{
    [SerializeField] RacketType powerupType;
    public static Action<RacketType> OnRacketPowerupPickup;

    protected override void InvokeEvent() => OnRacketPowerupPickup?.Invoke(powerupType);
}
