using System;
using Player.BallTypes;
using UnityEngine;

namespace Powerups
{
    public class BallPowerup : Pickup
    {
        public static Action<BallType> OnBallPowerupPickup;

        [SerializeField] BallType powerupType;

        protected override void InvokeEvent()
        {
            OnBallPowerupPickup?.Invoke(powerupType);
        }
    }
}