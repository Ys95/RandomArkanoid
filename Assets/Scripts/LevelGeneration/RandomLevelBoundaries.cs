using UnityEngine;

namespace LevelGeneration
{
    public class RandomLevelBoundaries : MonoBehaviour
    {
        [SerializeField] Transform boundaries;
        [SerializeField] GameObject defaultBoundary;
        int _boundariesAmount;

        GameObject _currentlyActive;

        void Awake()
        {
            _currentlyActive = defaultBoundary;
            _boundariesAmount = boundaries.transform.childCount;
        }

        public void ChooseRandomBoundary()
        {
            int roll = Random.Range(0, _boundariesAmount);

            _currentlyActive.SetActive(false);
            _currentlyActive = boundaries.GetChild(roll).gameObject;
            _currentlyActive.SetActive(true);
        }
    }
}