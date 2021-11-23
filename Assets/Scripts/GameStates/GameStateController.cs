using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateController : MonoBehaviour
{
    [SerializeField] GameState defaultState;
    Stack<GameState> _gameStates;

    void OnEnable()
    {
        _gameStates = new Stack<GameState>();
        _gameStates.Push(defaultState);
        defaultState.EnableState();
    }

    public void GoToNewState(GameState state)
    {
        _gameStates.Peek().DisableState();
        _gameStates.Push(state);
        state.EnableState();
    }

    public void GoToPreviousState()
    {
        if (_gameStates.Peek() == defaultState) return;

        _gameStates.Pop().DisableState();
        _gameStates.Peek().EnableState();
    }

    public void GoToDefault()
    {
        _gameStates.Pop().DisableState();
        _gameStates.Clear();
        _gameStates.Push(defaultState);
        defaultState.EnableState();
    }

    //Called from input event
    public void PauseKeyPress(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        _gameStates.Peek().HandlePauseKeyPress(context);
    }

    public void AnyKeyPress(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        _gameStates.Peek().HandleAnyKeyPress(context);
    }
}