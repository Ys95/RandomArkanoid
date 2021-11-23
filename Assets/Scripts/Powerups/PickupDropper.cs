using System.Collections.Generic;
using UnityEngine;

public class PickupDropper : MonoBehaviour
{
    [Range(0, 100)] [SerializeField] int pickupDropChance;
    
    [Space]
    [SerializeField] List<GameObject> pickupList;
    
    bool ShouldPickupDrop => Random.Range(0, 101) <= pickupDropChance;

    public void RollPickup(Vector2 pos, int score)
    {
        if (!ShouldPickupDrop) return;
        var whichPickup = Random.Range(0, pickupList.Count);

        DropPickup(pickupList[whichPickup], pos);
    }

    void DropPickup(GameObject pickup, Vector2 pos)
    {
        var drop = Instantiate(pickup, transform, true);
        drop.transform.position = pos;
    }

    public void WipePickups()
    {
        var pickups = new Transform[transform.childCount];

        var i = 0;
        foreach (Transform pickup in transform)
        {
            pickups[i] = pickup;
            i++;
        }

        foreach (var pickup in pickups) Destroy(pickup.gameObject);
    }
}