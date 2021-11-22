using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LostLifeState : GameState
{
    [Space]
    [SerializeField] LivesSystem livesSystem;
    [SerializeField] TextMeshProUGUI livesDisplay;
    
    void UpdateLivesDisplay() => livesDisplay.text = livesSystem.LivesLeft.ToString();

    protected override void OnStateEnter()
    {
        GameManager.PauseGame(true);
    }
    
    public override void HandleAnyKeyPress(InputAction.CallbackContext context) => GameStateController.GoToPreviousState();

    void Update()
    {
        UpdateLivesDisplay();
    }
}
