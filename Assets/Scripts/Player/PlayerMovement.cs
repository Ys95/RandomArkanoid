#if UNITY_ANDROID
#define ANDROID_CONTROLS
#endif

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
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
    
    [Serializable]
    class TouchScreenMovement : MovementType
    {
        [SerializeField] float smoothing;

        public override Vector2 GetMovementValue(float x, Rigidbody2D rb)
        {
            var velocityX = Mathf.Lerp(x * MovementSpeed, rb.velocity.x, smoothing);
            return new Vector2(velocityX, 0f);
        }
    }

    [Header("Components")]
    [SerializeField] Rigidbody2D playerRigidbody;

    [Space]
    [SerializeField] MouseMovement mouseMovement;
    [SerializeField] KeyboardMovement keyboardMovement;
    [SerializeField] TouchScreenMovement touchScreenMovement;

    float _inputAxisX;
    MovementType _movementType;


    void Awake()
    {
#if ANDROID_CONTROLS
        _movementType = touchScreenMovement;
#else
        _movementType = mouseMovement;
#endif
    }

    void FixedUpdate()
    {
        playerRigidbody.velocity = _movementType.GetMovementValue(_inputAxisX, playerRigidbody);
    }

    public void SetKeyboardInputAxisX(InputAction.CallbackContext context)
    {
#if !ANDROID_CONTROLS
         _movementType = keyboardMovement;
         _inputAxisX = context.ReadValue<float>();
#endif
    }

    public void SetMouseInputAxisX(InputAction.CallbackContext context)
    {
#if !ANDROID_CONTROLS
         _movementType = mouseMovement;
        _inputAxisX = context.ReadValue<float>();
#endif
    }

    public void SetTouchInputAxisX(InputAction.CallbackContext context)
    {
#if ANDROID_CONTROLS
        _movementType = touchScreenMovement;
        _inputAxisX = context.ReadValue<float>();
#endif
    }
}