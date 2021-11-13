using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketPowerup : PowerUp
{
    [SerializeField] RacketMode _powerupMode;
    public static Action<RacketMode> OnRacketPowerupPickup;

    protected override void InvokeEvent() => OnRacketPowerupPickup?.Invoke(_powerupMode);
}
