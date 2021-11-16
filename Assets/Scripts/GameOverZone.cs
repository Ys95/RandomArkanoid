using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    [SerializeField] BallScript ball;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag(Tags.Ball)) return;

        ball.GetBall.Model.BallCollider.enabled = false;
        Debug.Log("GameOver");
    }
}
