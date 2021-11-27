using System.Collections.Generic;
using UnityEngine;

namespace Powerups
{
    public class PickupDropper : MonoBehaviour
    {
        [Range(0, 100)] [SerializeField] int pickupDropChance;

        [Space]
        [SerializeField] List<GameObject> pickupList;

        bool ShouldPickupDrop => Random.Range(0, 101) <= pickupDropChance;

        public void RollPickup(Vector2 pos, int score)
        {
            if (!ShouldPickupDrop) return;
            int whichPickup = Random.Range(0, pickupList.Count);

            DropPickup(pickupList[whichPickup], pos);
        }

        void DropPickup(GameObject pickup, Vector2 pos)
        {
            GameObject drop = Instantiate(pickup, transform, true);
            drop.transform.position = pos;
        }

        public void WipePickups()
        {
            Transform[] pickups = new Transform[transform.childCount];

            int i = 0;
            foreach (Transform pickup in transform)
            {
                pickups[i] = pickup;
                i++;
            }

            foreach (Transform pickup in pickups)
            {
                Destroy(pickup.gameObject);
            }
        }
    }
}