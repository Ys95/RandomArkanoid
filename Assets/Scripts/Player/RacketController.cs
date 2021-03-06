using Player.RacketTypes;
using Powerups;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class RacketController : MonoBehaviour
    {
        [SerializeField] Racket racket;

        RacketType _currentRacketType;

        void OnEnable()
        {
            _currentRacketType = racket.DefaultRacketType;
            _currentRacketType.InitDefaultMode(racket);
            RacketPowerup.OnRacketPowerupPickup += ApplyPowerupMode;
        }

        void OnDisable()
        {
            RacketPowerup.OnRacketPowerupPickup -= ApplyPowerupMode;
            RestoreDefaultState();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Ball)) _currentRacketType.HandleBallCollision(other);
        }

        public void FireAction(InputAction.CallbackContext context)
        {
            _currentRacketType.HandleFireAction(context);
        }

        public void RestoreDefaultState()
        {
            _currentRacketType.OnModeExit();
            _currentRacketType = racket.DefaultRacketType;
            _currentRacketType.OnModeEnter(racket);
        }

        void ApplyPowerupMode(RacketType type)
        {
            if (_currentRacketType == type) return;

            _currentRacketType.OnModeExit();
            _currentRacketType = type;
            _currentRacketType.OnModeEnter(racket);
        }
    }
}