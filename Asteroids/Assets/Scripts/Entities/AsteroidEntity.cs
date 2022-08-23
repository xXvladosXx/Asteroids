using System;
using AsteroidZenject;
using AudioSystem;
using Combat;
using Combat.Core;
using Combat.Projectiles;
using Combat.Projectiles.Core;
using Data.Asteroid;
using Entities.Core;
using ObjectPoolers;
using Spawners.Core;
using StatsSystem.Core;
using UnityEngine;
using Utilities.Extensions;
using Zenject;
using Random = UnityEngine.Random;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D),
        typeof(SpriteRenderer))]
    public class AsteroidEntity : Entity, IDamagable
    {
        [field: SerializeField] public AsteroidData AsteroidData { get; private set; }
        [field: SerializeField] public float Size { get; set; }
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public event Action<AsteroidEntity> OnAsteroidDestroyed;
        public event Action<AsteroidEntity> OnAsteroidReleased;
        
        public void ReceiveDamage(HitData hitData)
        {
            Die();
        }
        
        public override void Die()
        {
            var explosion = ExplosionPool.Instance.GetPrefab();
            explosion.transform.position = transform.position;
            explosion.Play();
            this.CallWithDelay((() => ExplosionPool.Instance.ReleasePrefab(explosion)), 1f);
            AudioManager.Instance.PlayEffectSound(AsteroidData.AudioClip);

            OnAsteroidDestroyed?.Invoke(this);
        }

        private void Start()
        {
            _spriteRenderer.sprite = AsteroidData.PossibleSprites[Random.Range(0, AsteroidData.PossibleSprites.Length)];
            transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
        }

        public void SetDirection(Vector3 direction)
        {
            _rigidbody.AddForce(direction * RandomizeSpeed());
            
            this.CallWithDelay(ReleaseAsteroid, AsteroidData.Lifetime);
        }
        public void RandomizeSize()
        {
            var randomSize = Random.Range(AsteroidData.MinSize, AsteroidData.MaxSize);
            Size = randomSize;
            transform.localScale = Vector3.one * randomSize;

            _rigidbody.mass = randomSize;
        }
        
        public void CreateSplit(AsteroidEntity asteroidEntity)
        {
            Vector2 position = transform.position;
            position += Random.insideUnitCircle * .5f;

            asteroidEntity.transform.position = position;
            asteroidEntity.Size = Size * .5f;
            asteroidEntity.transform.localScale = Vector3.one * asteroidEntity.Size;

            asteroidEntity._rigidbody.mass = asteroidEntity.Size;
            asteroidEntity.SetDirection(Random.insideUnitCircle.normalized * RandomizeSpeed());
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
            if (col.transform.TryGetComponent(out IDamagable hurtable))
            {
                hurtable.ReceiveDamage(new HitData
                {
                    Damage = StatsData.GetStat(Stats.Damage)
                });
                
                Die();
            }
        }
    }
}
