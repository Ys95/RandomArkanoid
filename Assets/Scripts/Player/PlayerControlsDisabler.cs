using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsDisabler : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    
    public void DisableControls() => input.DeactivateInput();

    public void EnableControls() =>input.ActivateInput();
    
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
