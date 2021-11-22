using System;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public static Action<Vector2, int> OnBrickDestroyed;

    [SerializeField] BrickType brick;
    
    Dictionary<BrickNames, BrickType> _attachedBrickTypes;

    public bool IsBrickActive => brick.gameObject.activeInHierarchy;
    public BrickType Brick => brick;

    void GetAttachedBrickTypes()
    {
        _attachedBrickTypes = new Dictionary<BrickNames, BrickType>();
        
        foreach (Transform child in transform)
        {
            BrickType brickType = child.GetComponent<BrickType>();
            if(brickType == null) continue;
            
            Debug.Log("Adding: " + brickType.BrickName);
            _attachedBrickTypes.Add(brickType.BrickName, brickType);
        }
    }
    
    void Awake()
    {
        GetAttachedBrickTypes();
    }

    public void RestoreBrick() => brick.gameObject.SetActive(true);
    
    public void BrickHit(Collider2D other) => brick.HandleOnCollisionEnter(other);
    
    public void DisableBricks()
    {
        foreach (KeyValuePair<BrickNames, BrickType> brickType in _attachedBrickTypes)
        {
            brickType.Value.gameObject.SetActive(false);
        }
    }

    public void InvokeBrickDestroyedEvent(Vector2 pos, int score) => OnBrickDestroyed.Invoke(pos, score);

    public void ChangeBrickType(BrickNames brickName)
    {
        if (_attachedBrickTypes.ContainsKey(brickName))
        {
            brick = _attachedBrickTypes[brickName];
            return;
        }
        Debug.LogError(brickName + " not found");
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag(Tags.Ball)) return;
        brick.HandleOnCollisionEnter(collision.collider);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(Tags.Ball)) return;
        brick.HandleOnCollisionEnter(other);
    }
}
