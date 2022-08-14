using System;
using Combat;
using Combat.Projectiles;
using Combat.Projectiles.Core;
using Data.Asteroid;
using Entities.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D),
        typeof(SpriteRenderer))]
    public class AsteroidEntity : Entity
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
            if (Size > AsteroidData.SizeToSplit)
            {
                CreateSplit();
                CreateSplit();
            }
            
            Destroy(gameObject);
        }

        private void CreateSplit()
        {
            Vector2 position = transform.position;
            position += Random.insideUnitCircle * .5f;
            
            var half = Instantiate(this, position, transform.rotation);
            half.Size = Size * .5f;
            half.SetDirection(Random.insideUnitCircle.normalized * RandomizeSpeed());
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
            _rigidbody.AddForce(direction * RandomizeSpeed());
            
            Destroy(gameObject, AsteroidData.Lifetime);
        }

        private float RandomizeSpeed()
        {
            var possibleSpeed = Random.Range(AsteroidData.MinSpeed, AsteroidData.MaxSpeed);
            return possibleSpeed;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.TryGetComponent(out PlayerEntity playerEntity))
            {
                playerEntity.Die();
                Die();
            }
        }
    }
}
