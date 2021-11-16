using UnityEngine;

[CreateAssetMenu(fileName = "Default_BallType", menuName = "BallType/DefaultBallType")]
public class BallType : ScriptableObject
{
    [SerializeField] GameObject modelPrefab;
    [SerializeField] float modeSpeedModifier;
    
    Ball _ball;
    BallModel _model;

    protected Ball Ball => _ball;
    
    public void HandleOnCollisionEnter(Collision2D collision)
    {
        if (collision.collider.CompareTag(Tags.Brick)) HandleBrickCollision(collision.collider);
        else if (collision.collider.CompareTag(Tags.Border)) HandleBorderCollision(collision.collider);
    }
    
    void ClampVelocity() => Vector2.ClampMagnitude(_ball.Rigidbody.velocity, _ball.MaxSpeed);


    public virtual void HandleOnTriggerEnter(Collider2D collider)
    {
        if (collider.CompareTag(Tags.Racket)) HandleRacketCollision(collider);
    }
    
    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth) => (ballPos.x - racketPos.x) / racketWidth;
    
    protected virtual void HandleRacketCollision(Collider2D collider)
    {
        _ball.IncreaseMaxSpeed();
        float x = HitFactor(_ball.Transform.position, collider.transform.position, collider.bounds.size.x);
        Vector2 dir = new Vector2(x, 1f);
        _ball.Rigidbody.velocity = dir * _ball.MaxSpeed;
        
        _ball.Model.PlayOnRacketHitParticle();
        _ball.Model.PlayOnRacketHitSound();
    }

    protected virtual void HandleBrickCollision(Collider2D collider)
    {
        _ball.Model.PlayOnCollisionParticle();
        _ball.Model.PlayOnCollisionSound();
    }
    
    protected virtual void HandleBorderCollision(Collider2D collider)
    {
        _ball.Model.PlayOnCollisionParticle();
        _ball.Model.PlayOnBorderHitSound();
    }
    
    void SetSpeed()
    {
        if(_ball.Rigidbody.velocity != Vector2.zero) return;
        _ball.Rigidbody.velocity = Vector2.up * _ball.MaxSpeed;
    }
    
    void SwapModels()
    {
        GameObject gObject = Instantiate(modelPrefab, _ball.Transform, true);

        _model = gObject.GetComponent<BallModel>();
        _ball.Model = _model;
        _ball.Model.transform.position = _ball.Transform.position;
        _model.gameObject.SetActive(true);
    }  
    
    public virtual void OnModeEnter(Ball ball)
    {
        _ball = ball;
        _ball.ApplySpeedMod(modeSpeedModifier);
        SwapModels();
        SetSpeed();
    }
    
    public virtual void OnModeExit()
    {
        _ball.ApplySpeedMod(-1f*modeSpeedModifier);
        Destroy(_model.gameObject);
    }

    public void InitDefaultMode(Ball ball)
    {
        _ball = ball;
        _model = _ball.Model;
    }
}
