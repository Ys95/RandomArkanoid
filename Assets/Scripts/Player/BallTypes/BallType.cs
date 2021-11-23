using UnityEngine;

[CreateAssetMenu(fileName = "Default_BallType", menuName = "BallType/DefaultBallType")]
public class BallType : ScriptableObject
{
    [SerializeField] GameObject modelPrefab;
    [SerializeField] float modeSpeedModifier;
    
    BallModel _model;
    
    protected Ball Ball { get; private set; }

    public void HandleOnCollisionEnter(Collision2D collision)
    {
        if (collision.collider.CompareTag(Tags.Brick)) HandleBrickCollision(collision.collider);
        else if (collision.collider.CompareTag(Tags.Border)) HandleBorderCollision(collision.collider);
    }

    public virtual void HandleOnTriggerEnter(Collider2D collider)
    {
        if (collider.CompareTag(Tags.Racket)) HandleRacketCollision(collider);
    }

    float BounceAngle(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    protected virtual void HandleRacketCollision(Collider2D collider)
    {
        Ball.IncreaseMaxSpeed();
        var x = BounceAngle(Ball.Transform.position, collider.transform.position, collider.bounds.size.x);
        var dir = new Vector2(3f * x, 1f);
        Ball.Rigidbody.velocity = Vector2.ClampMagnitude(dir * Ball.MaxSpeed, Ball.MaxSpeed);

        Ball.Model.PlayOnBounceParticle();
        Ball.Model.PlayOnBounceSound();
    }

    protected virtual void HandleBrickCollision(Collider2D collider)
    {
        Ball.Model.PlayOnBrickHitSound();
        Ball.Model.PlayOnBrickHitParticle();
        ;
    }

    protected virtual void HandleBorderCollision(Collider2D collider)
    {
        Ball.Model.PlayOnBounceParticle();
        Ball.Model.PlayOnBounceSound();
    }

    void SetSpeed()
    {
        if (Ball.Rigidbody.velocity != Vector2.zero) return;
        Ball.Rigidbody.velocity = Vector2.up * Ball.MaxSpeed;
    }

    void SwapModels()
    {
        var gObject = Instantiate(modelPrefab, Ball.Transform, true);

        _model = gObject.GetComponent<BallModel>();
        Ball.Model = _model;
        Ball.Model.transform.position = Ball.Transform.position;
        _model.gameObject.SetActive(true);
    }

    public virtual void OnModeEnter(Ball ball)
    {
        Ball = ball;
        Ball.ApplySpeedMod(modeSpeedModifier);
        SwapModels();
        SetSpeed();
    }

    public virtual void OnModeExit()
    {
        Ball.ApplySpeedMod(-1f * modeSpeedModifier);
        Destroy(_model.gameObject);
    }

    public void InitDefaultMode(Ball ball)
    {
        Ball = ball;
        _model = Ball.Model;
    }
}