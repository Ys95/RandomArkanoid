using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;

    [Space]
    [SerializeField] MovementType mouseMovement;
    [SerializeField] MovementType keyboardMovement;

    MovementType _movementType;
    float _inputAxisX;
    
    [System.Serializable]
    struct MovementType
    {
        [SerializeField] float movementSpeed;
        
        public Vector2 GetMovementValue(float x)
        {
            return new Vector2(x * movementSpeed, 0f);
        }
    }
    
    public void SetInputAxisX(InputAction.CallbackContext context)
    {
        _movementType = keyboardMovement;
        _inputAxisX = context.ReadValue<float>();
    }

    public void SetMouseInputAxisX(InputAction.CallbackContext context)
    {
        _movementType = mouseMovement;
            _inputAxisX = context.ReadValue<float>();

    }

    void FixedUpdate()
    {
        rb.velocity = _movementType.GetMovementValue(_inputAxisX);
    }
}