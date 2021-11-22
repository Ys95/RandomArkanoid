using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GameState : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] GameStateController gameStateController;
    [SerializeField] GameObject displayedUI;
    
    protected GameStateController GameStateController => gameStateController;

    void OnValidate()
    {
        var controller = transform.parent.GetComponent<GameStateController>();
        if (controller != null) gameStateController = controller;
    }

    public virtual void HandlePauseKeyPress(InputAction.CallbackContext context)
    {
    }

    public virtual void HandleAnyKeyPress(InputAction.CallbackContext context)
    {
    }
    
    public void Open() => gameStateController.GoToNewState(this);

    public void Close() => gameStateController.GoToPreviousState();

    protected virtual void OnStateEnter()
    {
    }

    protected virtual void OnStateExit()
    {
    }
    
    public void EnableState()
    {
        if (displayedUI != null) displayedUI.SetActive(true);
        OnStateEnter();
    }

    public void DisableState()
    {
        if (displayedUI != null) displayedUI.SetActive(false);
        OnStateExit();
    }
}