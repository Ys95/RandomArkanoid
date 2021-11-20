using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] UiState defaultState;
    Stack<UiState> _uiStates;

    void OnEnable()
    {
        _uiStates = new Stack<UiState>();
        _uiStates.Push(defaultState);
        defaultState.EnableState();
    }

    public void GoToNewState(UiState state)
    {
        _uiStates.Peek().DisableState();
        _uiStates.Push(state);
        state.EnableState();
    }

    public void GoToPreviousState()
    {
        if (_uiStates.Peek() == defaultState) return;

        _uiStates.Pop().DisableState();
        _uiStates.Peek().EnableState();
    }
    
    public void GoToDefault()
    {
        _uiStates.Pop().DisableState();
        _uiStates.Clear();
        _uiStates.Push(defaultState);
        defaultState.EnableState();
    }

    //Called from input event
    public void PauseKeyPress(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        _uiStates.Peek().HandlePauseKeyPress(context);
    }
    
    public void AnyKeyPress(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        _uiStates.Peek().HandleAnyKeyPress(context);
    }
}