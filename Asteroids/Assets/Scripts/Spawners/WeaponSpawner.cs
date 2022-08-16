using Interaction.Weapon;
using Spawners.Core;
using UnityEngine;

namespace Spawners
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class WeaponSpawner : Spawner
    {
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private WeaponObject[] _weaponObjects;
        [SerializeField] private float _timeToDestroyWeapon;
        protected override void Awake()
        {
            base.Awake();

            _collider2D = GetComponent<Collider2D>();
        }

        public override void Spawn()
        {
            var xPoint = Random.Range(_collider2D.bounds.min.x, _collider2D.bounds.max.x);
            var yPoint = Random.Range(_collider2D.bounds.min.y, _collider2D.bounds.max.y);
            
            var vectorToSpawn = new Vector2(xPoint, yPoint);

            for (int i = 0; i < SpawnAmount; i++)
            {
                var weaponToSpawn = Random.Range(0, _weaponObjects.Length);
                var weaponObject = Instantiate(_weaponObjects[weaponToSpawn], vectorToSpawn, Quaternion.identity);
                
                Destroy(weaponObject.gameObject, _timeToDestroyWeapon);
            } 
        }
    }
}