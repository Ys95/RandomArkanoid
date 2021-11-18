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

    public void WipePowerups()
    {
        Transform[] powerups = new Transform[transform.childCount];

        int i = 0;
        foreach (Transform powerup in transform)
        {
            powerups[i] = powerup;
            i++;
        }
        foreach (Transform powerup in powerups)
        {
            Destroy(powerup.gameObject);
        }
    }
}
