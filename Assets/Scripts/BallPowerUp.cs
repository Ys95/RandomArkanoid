using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPowerUp : PowerUp
{
    [SerializeField] BallMode _powerupMode;
    public static Action<BallMode> OnBallPowerupPickup;

    protected override void InvokeEvent() => OnBallPowerupPickup?.Invoke(_powerupMode);
}
