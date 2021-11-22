using UnityEngine;
using UnityEngine.InputSystem;

public class RacketController : MonoBehaviour
{
    [SerializeField] Racket racket;
        
    RacketType _currentRacketType;
    
    public void FireAction(InputAction.CallbackContext context) =>_currentRacketType.HandleFireAction(context);

    void OnEnable()
    {
        _currentRacketType = racket.DefaultRacketType;
        _currentRacketType.InitDefaultMode(racket);
        RacketPowerup.OnRacketPowerupPickup += ApplyPowerupMode;
    }
    
    public void RestoreDefaultState()
    {
        _currentRacketType.OnModeExit();
        _currentRacketType = racket.DefaultRacketType;
        _currentRacketType.OnModeEnter(racket);
    }

    void ApplyPowerupMode(RacketType type)
    {
        if(_currentRacketType == type) return;
        
        _currentRacketType.OnModeExit();
        _currentRacketType = type;
        _currentRacketType.OnModeEnter(racket);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(Tags.Ball)) _currentRacketType.HandleBallCollision(other);
    }

    void OnDisable()
    {
        RacketPowerup.OnRacketPowerupPickup -= ApplyPowerupMode;
        RestoreDefaultState();
    }
}
