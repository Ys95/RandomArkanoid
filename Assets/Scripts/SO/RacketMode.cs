using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default_RacketMode", menuName = "RacketModes/DefaultRacketMode")]
public class RacketMode : ScriptableObject
{
    [SerializeField] GameObject _modelPrefab;

    Racket _racket;
    RacketModel _thisModeModel;
    
    public virtual void HandleFireAction()
    {
    }

    public virtual void HandleBallCollision(Collider2D collider2D)
    {
    }

    public void InitDefaultMode(Racket racket)
    {
        _racket = racket;
        _thisModeModel = _racket.Model;
    }

    public void OnModeEnter(Racket racket)
    {
        if (_thisModeModel == null)
        {
            GameObject gameObject = Instantiate(_modelPrefab, racket.Transform, true);
            _thisModeModel = gameObject.GetComponent<RacketModel>();
        }
        _racket = racket;
        
        _racket.Model = _thisModeModel;
        _racket.Model.transform.position = _racket.Transform.position;
        _thisModeModel.gameObject.SetActive(true);
    }

    public void OnModeExit()
    {
        _racket.Model.gameObject.SetActive(false);
    }
}
