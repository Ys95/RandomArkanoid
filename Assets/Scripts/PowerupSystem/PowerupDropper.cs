using UnityEngine;
using System.Collections.Generic;

public class PowerupDropper : MonoBehaviour
{
    [Range(0, 100)] [SerializeField] int powerupDropChance;

    [Space]
    [SerializeField] List<GameObject> powerupList;

    bool ShouldPowerupDrop => (Random.Range(0, 101) <= powerupDropChance);

    public void RollPowerup(Vector2 pos)
    {
        if (!ShouldPowerupDrop) return;
        int whichPowerup = Random.Range(0, powerupList.Count);
            
        DropPowerup(powerupList[whichPowerup], pos);  
    }

    void DropPowerup(GameObject powerup, Vector2 pos)
    {
        GameObject drop = Instantiate(powerup, transform, true);
        drop.transform.position = pos;
    }
}
