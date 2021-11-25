using System;
using Player.RacketTypes;
using UnityEngine;

namespace Powerups
{
    public class RacketPowerup : Pickup
    {
        public static Action<RacketType> OnRacketPowerupPickup;

        [SerializeField] RacketType powerupType;

        protected override void InvokeEvent()
        {
            OnRacketPowerupPickup?.Invoke(powerupType);
        }
    }
}