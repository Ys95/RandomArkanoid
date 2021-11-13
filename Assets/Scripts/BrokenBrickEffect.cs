using UnityEngine;

public class BrokenBrickEffect : MonoBehaviour
{
    [SerializeField] BrickPiece[] _brickPieces;

    [Space]
    [SerializeField] float _minTorque;
    [SerializeField] float _maxTorque;
    [SerializeField] float _effectDuration;

    [System.Serializable]
    struct BrickPiece
    {
        [SerializeField] public Rigidbody2D Rigidbody;
        [SerializeField] public Transform InitialTransform;
    }

    public void Play()
    {
        if (_brickPieces == null) return;

        Vector2 xForceDir = new Vector2(0.5f, 0f);
        float torqueDir = 1f;

        foreach (BrickPiece brickPiece in _brickPieces)
        {
            float torque = Random.Range(_minTorque, _maxTorque);
            
            brickPiece.Rigidbody.gameObject.SetActive(true);

            brickPiece.Rigidbody.constraints = RigidbodyConstraints2D.None;

            //brickPiece.Rigidbody.AddForce(xForceDir, ForceMode2D.Impulse);
            brickPiece.Rigidbody.AddTorque(torque*torqueDir);

            torqueDir*= -1f;
            xForceDir *= -1f;
        }

        Invoke(nameof(Stop), _effectDuration);
    }

    void Stop()
    {
        foreach (BrickPiece brickPiece in _brickPieces)
        {
            brickPiece.Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            brickPiece.Rigidbody.gameObject.transform.localPosition = brickPiece.InitialTransform.localPosition;
        }

        gameObject.SetActive(false);
    }
}