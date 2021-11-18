using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    [SerializeField] LivesSystem livesSystem;
    
    [SerializeField] BallController ball;
    [SerializeField] Collider2D trigger;

    public void Disable() => trigger.enabled = false;

    public void Enable() => trigger.enabled = true;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag(Tags.Ball)) return;
        ball.GetBall.Model.BallCollider.enabled = false;
        Debug.Log("Life lost");
        livesSystem.LoseLife();
    }
}
