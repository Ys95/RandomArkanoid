using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class UiState : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] UIController uiController;
    [SerializeField] GameObject thisMenuGameObject;
    
    protected UIController UiController => uiController;

    void OnValidate()
    {
        var controller = transform.parent.GetComponent<UIController>();
        if (controller != null) uiController = controller;
    }

    public virtual void HandlePauseKeyPress(InputAction.CallbackContext context)
    {
    }

    public virtual void HandleAnyKeyPress(InputAction.CallbackContext context)
    {
    }
    
    public void Open() => uiController.GoToNewState(this);

    public void Close() => uiController.GoToPreviousState();

    protected virtual void OnStateEnter()
    {
    }

    protected virtual void OnStateExit()
    {
    }
    
    public void EnableState()
    {
        if (thisMenuGameObject != null) thisMenuGameObject.SetActive(true);
        OnStateEnter();
    }

    public void DisableState()
    {
        if (thisMenuGameObject != null) thisMenuGameObject.SetActive(false);
        OnStateExit();
    }
}