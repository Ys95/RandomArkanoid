using UnityEngine;

public class LivesSystem : MonoBehaviour
{
    [SerializeField] int initialLivesAmount;

    public int LivesLeft { get; private set; }

    public void AddChance()
    {
        LivesLeft++;
    }

    public void LoseLife()
    {
        LivesLeft--;

        if (LivesLeft >= 0) GameManager.Instance.LoseLife();
        else GameManager.Instance.GameOver();
    }

    public void ResetChances()
    {
        LivesLeft = initialLivesAmount;
    }
}