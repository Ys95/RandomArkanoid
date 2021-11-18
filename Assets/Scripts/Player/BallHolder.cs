using UnityEngine;
using UnityEngine.InputSystem;

public class BallHolder : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject ballGameObject;
    [SerializeField] BallController ball;
    [SerializeField] InputActionAsset input;

    InputActionMap _defaultMap;
    InputAction _fire;

    void Awake()
    {
        _defaultMap = input.FindActionMap("Default");
        _fire = _defaultMap.FindAction("Fire");
    }

    void OnEnable()
    {
        AttachBall();
        _fire.performed += PushBall;
    }

    void OnDisable()
    {
        _fire.performed -= PushBall;
    }

    public void AttachBall()
    {
        Debug.Log("Ball reattached");
        ball.GetBall.StopMoving();
        
        var holderTransform = transform;
        
        ballGameObject.transform.parent = holderTransform;
        ballGameObject.transform.position = holderTransform.position;
        _fire.performed += PushBall;
    }

    void PushBall(InputAction.CallbackContext context)
    {
        ball.transform.parent = player.transform;
        ball.GetBall.StartMoving();
        Debug.Log("Ball pushed");
        _fire.performed -= PushBall;
    }
}