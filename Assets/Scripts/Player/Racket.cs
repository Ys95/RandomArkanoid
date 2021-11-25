using System;
using Player.RacketTypes;
using Player.RacketTypes.Models;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class Racket
    {
        [Header("Components")]
        [SerializeField] RacketModel model;
        [SerializeField] Rigidbody2D rb;
        [SerializeField] Transform transform;

        [Space]
        [SerializeField] Transform player;
        [SerializeField] RacketType defaultRacketType;

        public Transform Transform => transform;

        public RacketModel Model
        {
            get => model;
            set => model = value;
        }

        public Rigidbody2D Rb => rb;
        public Transform Player => player;
        public RacketType DefaultRacketType => defaultRacketType;
    }
}