using System;
using AudioSystem;
using Combat;
using Combat.Projectiles;
using Combat.Projectiles.Core;
using Data.Asteroid;
using Entities.Core;
using ObjectPoolers;
using Spawners.Core;
using UnityEngine;
using Utilities.Extensions;
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

        public event Action<AsteroidEntity> OnAsteroidDestroyed;
        public event Action<AsteroidEntity> OnAsteroidReleased;
        protected override void Awake()
        {
            base.Awake();

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public override void Die()
        {
            var explosion = ExplosionPool.Instance.GetPrefab();
            explosion.transform.position = transform.position;
            explosion.Play();
            this.CallWithDelay((() => ExplosionPool.Instance.ReleasePrefab(explosion)), 1f);
            AudioManager.Instance.PlayEffectSound(AsteroidData.AudioClip);
            
            if (Size > AsteroidData.SizeToSplit)
            {
                CreateSplit();
                CreateSplit();
            }

            if (OnAsteroidDestroyed == null)
            {
                Destroy(gameObject);
                return;
            }
            
            OnAsteroidDestroyed?.Invoke(this);
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
            
            this.CallWithDelay(ReleaseAsteroid, AsteroidData.Lifetime);
        }

        private void ReleaseAsteroid()
        {
            OnAsteroidReleased?.Invoke(this);
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
