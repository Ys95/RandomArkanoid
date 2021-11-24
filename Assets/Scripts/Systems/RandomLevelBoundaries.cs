using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomLevelBoundaries : MonoBehaviour
{
    [SerializeField] Transform boundaries;
    [SerializeField] GameObject defaultBoundary;

    GameObject _currentlyActive;
    int _boundariesAmount;

    void Awake()
    {
        _currentlyActive = defaultBoundary;
        _boundariesAmount = boundaries.transform.childCount;
    }

    public void ChooseRandomBoundary()
    {
        var roll = Random.Range(0, _boundariesAmount);
        
        _currentlyActive.SetActive(false);
        _currentlyActive = boundaries.GetChild(roll).gameObject;
        _currentlyActive.SetActive(true);
    }
}
