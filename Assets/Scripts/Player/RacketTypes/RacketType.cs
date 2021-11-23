using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Default_RacketType", menuName = "RacketType/DefaultRacketType")]
public class RacketType : ScriptableObject
{
    [SerializeField] GameObject modelPrefab;
    
    public Racket Racket { get; private set; }
    public RacketModel Model { get; private set; }

    public virtual void HandleFireAction(InputAction.CallbackContext context)
    {
    }

    public virtual void HandleBallCollision(Collider2D collider2D)
    {
    }

    public void InitDefaultMode(Racket racket)
    {
        Racket = racket;
        Model = Racket.Model;
    }

    public virtual void OnModeEnter(Racket racket)
    {
        if (Model == null)
        {
            var gameObject = Instantiate(modelPrefab, racket.Transform, true);
            Model = gameObject.GetComponent<RacketModel>();
        }

        Racket = racket;

        Racket.Model = Model;
        Racket.Model.transform.position = Racket.Transform.position;
        Model.gameObject.SetActive(true);
    }

    public virtual void OnModeExit()
    {
        Model.gameObject.SetActive(false);
    }
}