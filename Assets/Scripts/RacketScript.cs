using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Racket
{
    [Header("Components")] 
    [SerializeField] RacketModel _model;
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] Transform _transform;
    
    [Space] 
    [SerializeField] RacketMode _defaultRacketMode;
    
    public Transform Transform => _transform;

    public RacketModel Model
    {
        get => _model;
        set => _model = value;
    }
    
    public Rigidbody2D Rigidbody => _rigidbody;

    public RacketMode DefaultRacketMode => _defaultRacketMode;
}

public class RacketScript : MonoBehaviour
{
    [SerializeField] Racket _racket;
        
    RacketMode _currentRacketMode;
    
    public void FireAction(InputAction.CallbackContext context) =>_currentRacketMode.HandleFireAction();

    void OnEnable()
    {
        _currentRacketMode = _racket.DefaultRacketMode;
        _currentRacketMode.InitDefaultMode(_racket);
        RacketPowerup.OnRacketPowerupPickup += ApplyPowerupMode;
    }

    void ApplyPowerupMode(RacketMode mode)
    {
        _currentRacketMode.OnModeExit();
        _currentRacketMode = mode;
        _currentRacketMode.OnModeEnter(_racket);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(Tags.BALL)) _currentRacketMode.HandleBallCollision(other);
    }

    void OnDisable()
    {
        RacketPowerup.OnRacketPowerupPickup -= ApplyPowerupMode;
    }
}
