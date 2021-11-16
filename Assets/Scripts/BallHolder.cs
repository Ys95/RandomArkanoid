using UnityEngine;
using UnityEngine.InputSystem;

public class BallHolder : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject ballGameObject;
    [SerializeField] BallScript ball;
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

    void AttachBall()
    {
        ball.GetBall.StopMoving();
        ballGameObject.transform.parent = transform;
    }

    void PushBall(InputAction.CallbackContext context)
    {
        ball.transform.parent = player.transform;
        ball.GetBall.StartMoving();
        Debug.Log("Ball pushed");
        gameObject.SetActive(false);
    }
}