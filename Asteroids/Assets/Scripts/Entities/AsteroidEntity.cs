using System;
using Combat;
using Data.Asteroid;
using Entities.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D),
        typeof(SpriteRenderer))]
    public class AsteroidEntity : AliveEntity
    {
        [field: SerializeField] public AsteroidData AsteroidData { get; private set; }
        [field: SerializeField] public float Size { get; set; } 

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;

        protected override void Awake()
        {
            base.Awake();

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public override void Die()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            _spriteRenderer.sprite = AsteroidData.PossibleSprites[Random.Range(0, AsteroidData.PossibleSprites.Length)];

            transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
            transform.localScale = Vector3.one * Size;

            _rigidbody.mass = Size;
        }

        public void SetDirection(Vector3 direction)
        {
            var possibleSpeed = Random.Range(AsteroidData.MinSpeed, AsteroidData.MaxSpeed);
            _rigidbody.AddForce(direction * possibleSpeed);
            
            Destroy(gameObject, AsteroidData.Lifetime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.TryGetComponent(out PlayerEntity playerEntity))
            {
                playerEntity.Die();
                Die();
            }

            if (col.transform.TryGetComponent(out Bullet bullet))
            {
                Die();
            }
        }
    }
}
