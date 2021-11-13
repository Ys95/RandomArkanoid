using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public delegate void BrickDestroyed(Vector2 pos);
    public static event BrickDestroyed OnBrickDestroyed;

    [SerializeField] GameObject _brickVisuals;
    [SerializeField] ParticleSystem _destroyParticle;

    public void DestroyBrick()
    {
        _brickVisuals.SetActive(false);
        OnBrickDestroyed?.Invoke(transform.position);
        

        if (_destroyParticle == null)
        {
            DisableThis();
            return;
        }

        _destroyParticle.Play();
        Invoke(nameof(DisableThis), _destroyParticle.main.duration+0.01f);
    }

    void DisableThis() => gameObject.SetActive(false);

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag != Tags.BALL) return;
        DestroyBrick();
    }
}
