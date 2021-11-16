using UnityEngine;
using UnityEngine.InputSystem;


[System.Serializable]
public class Racket
{
    [Header("Components")] 
    [SerializeField] RacketModel model;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform transform;

    [Space] 
    [SerializeField] Transform player;
    [SerializeField] RacketType defaultRacketType;
    
    public Transform Transform => transform;

    public RacketModel Model
    {
        get => model;
        set => model = value;
    }
    
    public Rigidbody2D Rb => rb;
    public Transform Player => player;
    public RacketType DefaultRacketType => defaultRacketType;
}

public class RacketScript : MonoBehaviour
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
    }
}
