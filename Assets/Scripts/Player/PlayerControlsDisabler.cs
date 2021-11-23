using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsDisabler : MonoBehaviour
{
    [SerializeField] PlayerInput input;

    void OnEnable()
    {
        GameManager.OnGamePause += DisableControls;
        GameManager.OnGameUnpause += EnableControls;
    }

    void OnDisable()
    {
        GameManager.OnGamePause -= DisableControls;
        GameManager.OnGameUnpause -= EnableControls;
    }

    public void DisableControls()
    {
        input.DeactivateInput();
    }

    public void EnableControls()
    {
        input.ActivateInput();
    }
}