using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Default_RacketType", menuName = "RacketType/DefaultRacketType")]
public class RacketType : ScriptableObject
{
    [SerializeField] GameObject modelPrefab;

    Racket _racket;
    RacketModel _model;

    public Racket Racket => _racket;

    public RacketModel Model => _model;
        
    public virtual void HandleFireAction(InputAction.CallbackContext context)
    {
    }

    public virtual void HandleBallCollision(Collider2D collider2D)
    {
    }

    public void InitDefaultMode(Racket racket)
    {
        _racket = racket;
        _model = _racket.Model;
    }

    public void OnModeEnter(Racket racket)
    {
        if (_model == null)
        {
            GameObject gameObject = Instantiate(modelPrefab, racket.Transform, true);
            _model = gameObject.GetComponent<RacketModel>();
        }
        _racket = racket;
        
        _racket.Model = _model;
        _racket.Model.transform.position = _racket.Transform.position;
        _model.gameObject.SetActive(true);
    }

    public void OnModeExit()
    {
        _racket.Model.gameObject.SetActive(false);
    }
}
