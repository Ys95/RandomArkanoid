using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LostLifeState : GameState
{
    [Space]
    [SerializeField] LivesSystem livesSystem;
    [SerializeField] TextMeshProUGUI livesDisplay;

    void Update()
    {
        UpdateLivesDisplay();
    }

    void UpdateLivesDisplay()
    {
        livesDisplay.text = livesSystem.LivesLeft.ToString();
    }

    protected override void OnStateEnter()
    {
        GameManager.PauseGame(true);
    }

    public override void HandleAnyKeyPress(InputAction.CallbackContext context)
    {
        GameStateController.GoToPreviousState();
    }
}