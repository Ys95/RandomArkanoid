using UnityEngine;
using System.Collections.Generic;

public class PowerupDropper : MonoBehaviour
{
    [Range(0, 100)] [SerializeField] int _powerupDropChance;

    [Space]
    [SerializeField] List<GameObject> _powerupList;

    bool ShouldPowerupDrop => (Random.Range(0, 101) <= _powerupDropChance);

    public void RollPowerup(Vector2 pos)
    {
        if (!ShouldPowerupDrop) return;
        int whichPowerup = Random.Range(0, _powerupList.Count);
            
        DropPowerup(_powerupList[whichPowerup], pos);  
    }

    void DropPowerup(GameObject powerup, Vector2 pos)
    {
        GameObject drop = Instantiate(powerup);
        drop.transform.parent = null;
        drop.transform.position = pos;
    }
}
