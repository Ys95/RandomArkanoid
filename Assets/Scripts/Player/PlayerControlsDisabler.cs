using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsDisabler : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    
    void DisableControls() => input.DeactivateInput();

    void EnableControls() =>input.ActivateInput();
    
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
}
