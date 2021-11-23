using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    
    [Space]
    [SerializeField] MouseMovement mouseMovement;
    [SerializeField] KeyboardMovement keyboardMovement;
    
    float _inputAxisX;
    MovementType _movementType;

    void Awake()
    {
        _movementType = mouseMovement;
    }

    void FixedUpdate()
    {
        rb.velocity = _movementType.GetMovementValue(_inputAxisX, rb);
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

    [Serializable]
    abstract class MovementType
    {
        [SerializeField] float movementSpeed;
        public float MovementSpeed => movementSpeed;
        public abstract Vector2 GetMovementValue(float x, Rigidbody2D rb);
    }

    [Serializable]
    class MouseMovement : MovementType
    {
        [SerializeField] float smoothing;

        public override Vector2 GetMovementValue(float x, Rigidbody2D rb)
        {
            var velocityX = Mathf.Lerp(x * MovementSpeed, rb.velocity.x, smoothing);
            return new Vector2(velocityX, 0f);
        }
    }

    [Serializable]
    class KeyboardMovement : MovementType
    {
        public override Vector2 GetMovementValue(float x, Rigidbody2D rb)
        {
            return new Vector2(x * MovementSpeed, 0f);
        }
    }
}