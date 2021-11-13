using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ball
{
    [Header("Components")]
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] Collider2D _ballCollider;
    [SerializeField] Transform _transform;

    [Space] 
    [SerializeField] BallMode _defaultBallMode;
    [SerializeField] float _initialSpeed;
    [SerializeField] float _maxSpeedIncreasePerCollision;

    float _maxSpeed;
    public float MaxSpeed => _maxSpeed;
    public void SetMaxSpeed() =>_maxSpeed = _initialSpeed;

    public void InceaseMaxSpeed() => _maxSpeed += _maxSpeedIncreasePerCollision;

    public Rigidbody2D Rigidbody => _rigidbody;

    public Collider2D BallCollider => _ballCollider;

    public Transform Transform => _transform;

    public BallMode DefaultBallMode => _defaultBallMode;

    public float InitialSpeed => _initialSpeed;
}

public class BallScript : MonoBehaviour
{
    [SerializeField] Ball _ball;
    
    BallMode _currentBallMode;
    
    void OnEnable()
    {
        BallPowerUp.OnBallPowerupPickup += ApplyPowerup;
    }

    void Start()
    {
        _ball.SetMaxSpeed();
        _currentBallMode = _ball.DefaultBallMode;
        _currentBallMode.OnModeEnter(_ball);
    }

    void ApplyPowerup(BallMode newMode)
    {
        _currentBallMode.OnModeExit();
        _currentBallMode = newMode;
        _currentBallMode.OnModeEnter(_ball);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        _currentBallMode.HandleCollision(other);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _currentBallMode.HandleRacketCollision(other);
    }

    void OnDisable()
    {
        BallPowerUp.OnBallPowerupPickup -= ApplyPowerup;
    }
}
