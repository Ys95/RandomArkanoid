using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Laser_BallMode", menuName = "BallModes/LaserBallMode")]
public class LaserBallMode : BallMode
{
    Vector2 _ballTrajectory;
    
    protected override void HandleBrickCollision(Collision2D collision)
    {
        Ball.Rigidbody.velocity = Ball.MaxSpeed * _ballTrajectory;
    }

    public override void HandleRacketCollision(Collider2D racket)
    {
        base.HandleRacketCollision(racket);
        _ballTrajectory = Ball.Rigidbody.velocity.normalized;
    }

    protected override void HandleBorderCollision(Collision2D collision)
    {
        _ballTrajectory = Ball.Rigidbody.velocity.normalized;
    }

    public override void OnModeEnter(Ball ball)
    {
        base.OnModeEnter(ball);
        _ballTrajectory = Ball.Rigidbody.velocity.normalized;
    }
}