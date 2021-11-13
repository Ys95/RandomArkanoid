using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D _rb;

    [Space]
    [SerializeField] float _movementSpeed;

    float _inputAxisX;

    public void SetInputAxisX(InputAction.CallbackContext context) => _inputAxisX = context.ReadValue<float>();
    
    void FixedUpdate()
    {
        Vector2 newPos = new Vector2(_inputAxisX * _movementSpeed, 0f);
        _rb.velocity = newPos;
    }
}