using Interaction.Weapon;
using UnityEngine;
using Utilities.Extensions;

namespace Interaction
{
    [RequireComponent(typeof(Collider2D))]
    public class PickableSpawner : MonoBehaviour
    {
        [field: SerializeField] public int SpawnAmount { get; private set; }
        [field: SerializeField] public float SpawnRate { get; private set; }
        
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private PickableObject[] _pickableObjects;
        [SerializeField] private float _timeToDestroyWeapon;
        protected void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }
        
        private void Start()
        {
            this.CallWithRepeat(Spawn, SpawnRate);
        }

        public void Spawn()
        {
            var xPoint = Random.Range(_collider2D.bounds.min.x, _collider2D.bounds.max.x);
            var yPoint = Random.Range(_collider2D.bounds.min.y, _collider2D.bounds.max.y);
            
            var vectorToSpawn = new Vector2(xPoint, yPoint);

            for (int i = 0; i < SpawnAmount; i++)
            {
                var pickableToSpawn = Random.Range(0, _pickableObjects.Length);
                var pickableObject = Instantiate(_pickableObjects[pickableToSpawn], vectorToSpawn, Quaternion.identity);
                
                Destroy(pickableObject.gameObject, _timeToDestroyWeapon);
            } 
        }
    }
}