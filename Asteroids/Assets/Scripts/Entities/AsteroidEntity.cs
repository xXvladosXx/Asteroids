using System;
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
        
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;

        protected override void Awake()
        {
            base.Awake();

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _spriteRenderer.sprite = AsteroidData.PossibleSprites[Random.Range(0, AsteroidData.PossibleSprites.Length)];

            transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
            transform.localScale = Vector3.one * AsteroidData.PossibleSize;

            _rigidbody.mass = AsteroidData.PossibleSize;
        }
    }
}
