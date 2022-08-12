using System;
using Data.Bullet;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [field: SerializeField] public BulletData BulletData { get; private set; }
        
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Fire(Vector2 direction)
        {
            _rigidbody2D.AddForce(direction * BulletData.BulletSpeed);
            
            Destroy(gameObject, BulletData.MaxLifeTime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if(col.collider.GetComponent<Bullet>() != null) return;
            
            Destroy(gameObject);
        }
    }
}
