using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Ball ball;
    
    BallType _currentBallType;

    public Ball GetBall => ball;
    
    void OnEnable()
    {
        BallPowerUp.OnBallPowerupPickup += ApplyPowerup;
    }

    void Awake()
    {
        _currentBallType = ball.DefaultBallType;
        _currentBallType.InitDefaultMode(ball);
    }

    public void RestoreDefualtState()
    {
        _currentBallType.OnModeExit();
        _currentBallType = ball.DefaultBallType;
        ball.SetMaxSpeed();
        _currentBallType.OnModeEnter(ball);
    }

    void ApplyPowerup(BallType newType)
    {
        if(_currentBallType == newType) return;
        
        _currentBallType.OnModeExit();
        _currentBallType = newType;
        _currentBallType.OnModeEnter(ball);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        _currentBallType.HandleOnCollisionEnter(other);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _currentBallType.HandleOnTriggerEnter(other);
    }

    void OnDisable()
    {
        BallPowerUp.OnBallPowerupPickup -= ApplyPowerup;
    }
}