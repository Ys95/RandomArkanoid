using TMPro;
using UnityEngine;

public class DurableBrickType : BrickType
{
    [Space]
    [Header("Durable brick")]
    [SerializeField] TextMeshPro durabilityText;
    [SerializeField] GameObject destructibleLayer;

    [Space]
    [SerializeField] ParticleSystem onHitParticle;
    [SerializeField] SoundEffect onHitSoundEffect;

    [Space]
    [SerializeField] int minDurability;
    [SerializeField] int maxDurability;

    int _currentDurability;
    int _rolledDurability;

    int RollDurability => Random.Range(minDurability, maxDurability + 1);
    protected override int CalculateScore => _rolledDurability + Score;

    protected override void OnBrickEnabled()
    {
        base.OnBrickEnabled();
        _rolledDurability = RollDurability;
        _currentDurability = _rolledDurability;

        durabilityText.text = _currentDurability.ToString();
        destructibleLayer.SetActive(true);
    }

    void ReduceDurability()
    {
        _currentDurability--;

        if (_currentDurability == 0)
        {
            destructibleLayer.SetActive(false);
        }
        else if (_currentDurability < 0)
        {
            DestroyBrick();
        }
        else
        {
            durabilityText.text = _currentDurability.ToString();
            onHitParticle.Play();
            onHitSoundEffect.PlayDetached(BrickTransform.position);
        }
    }

    public override void HandleOnCollisionEnter(Collider2D other)
    {
        ReduceDurability();
    }
}