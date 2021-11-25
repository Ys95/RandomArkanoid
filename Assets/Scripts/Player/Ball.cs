using System;
using Player.BallTypes;
using Player.BallTypes.Models;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class Ball
    {
        [Header("Components")]
        [SerializeField] Rigidbody2D rigidbody;
        [SerializeField] Transform transform;
        [SerializeField] BallModel model;

        [Space]
        [SerializeField] BallType defaultBallType;
        [SerializeField] float initialSpeed;
        [SerializeField] float maxSpeedIncreasePerCollision;

        public float MaxSpeed { get; private set; }

        public BallModel Model
        {
            get => model;
            set => model = value;
        }

        public Rigidbody2D Rigidbody => rigidbody;
        public Transform Transform => transform;
        public BallType DefaultBallType => defaultBallType;
        public float InitialSpeed => initialSpeed;

        public void SetMaxSpeed()
        {
            MaxSpeed = initialSpeed;
        }

        public void StartMoving()
        {
            SetMaxSpeed();
            rigidbody.simulated = true;
            rigidbody.velocity = MaxSpeed * Vector2.up;
        }

        public void StopMoving()
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.simulated = false;
        }

        public void IncreaseMaxSpeed()
        {
            MaxSpeed += maxSpeedIncreasePerCollision;
        }

        public void ApplySpeedMod(float speedMod)
        {
            MaxSpeed += speedMod;
            var newRigidbodyVelocity = rigidbody.velocity.normalized * MaxSpeed;
            rigidbody.velocity = newRigidbodyVelocity;
        }
    }
}