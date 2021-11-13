using UnityEngine;

public abstract class BallMode : ScriptableObject
{
    Ball _ball;

    protected Ball Ball => _ball;
    
    public void HandleCollision(Collision2D collision)
    {
        ClampVelocity();
        if (collision.collider.CompareTag(Tags.BRICK)) HandleBrickCollision(collision);
        else if (collision.collider.CompareTag(Tags.BORDER)) HandleBorderCollision(collision);
    }
    
    void SetSpeed()
    {
        if(_ball.Rigidbody.velocity != Vector2.zero) return;
        _ball.Rigidbody.velocity = Vector2.up * _ball.MaxSpeed;
    }

    void ClampVelocity() => Vector2.ClampMagnitude(_ball.Rigidbody.velocity, _ball.MaxSpeed);

    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth) => (ballPos.x - racketPos.x) / racketWidth;
    
    public virtual void HandleRacketCollision(Collider2D racket)
    {
        _ball.InceaseMaxSpeed();
        float x = HitFactor(_ball.Transform.position, racket.transform.position, racket.bounds.size.x);
        Vector2 dir = new Vector2(x, 1f);
        _ball.Rigidbody.velocity = dir * _ball.MaxSpeed;
    }

    protected virtual void HandleBrickCollision(Collision2D collision) 
    {
    }
    
    protected virtual void HandleBorderCollision(Collision2D collision)
    {
    }
    
    public virtual void OnModeEnter(Ball ball)
    {
        _ball = ball;
        SetSpeed();
    }

    public virtual void OnModeExit()
    {
    }
}
