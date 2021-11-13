using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Big_BallMode", menuName = "BallModes/BigBallMode")]
public class BigBallMode : BallMode
{
    [SerializeField] Vector2 _sizeMultiplier;

    Vector2 _defaultBallSize;

    public override void OnModeEnter(Ball ball)
    {
        base.OnModeEnter(ball);
        
        var localScale = ball.Transform.localScale;
        
        _defaultBallSize = localScale;
        localScale *= _sizeMultiplier;
        ball.Transform.localScale = localScale;
    }

    public override void OnModeExit()
    {
        base.OnModeExit();
        Ball.Transform.localScale = _defaultBallSize;
    }
}
