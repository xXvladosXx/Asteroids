using Entities;
using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPoolers
{
    public sealed class AsteroidPool : MonoBehaviour
    {
        public static AsteroidPool Instance { get; private set; }
        
        [SerializeField] private AsteroidEntity _asteroidEntity;

        private ObjectPool<AsteroidEntity> _pool;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            MakePool();
        }

        private void MakePool()
        {
            _pool = new ObjectPool<AsteroidEntity>(() =>
                    Instantiate(_asteroidEntity, gameObject.transform),
                prefab => { prefab.gameObject.SetActive(true); },
                prefab => { prefab.gameObject.SetActive(false); }, 
                prefab => { Destroy(prefab.gameObject); }, 
                false, 100, 200);
        }

        public AsteroidEntity GetPrefab() => _pool.Get();

        public void ReleasePrefab(AsteroidEntity asteroidEntity) => _pool.Release(asteroidEntity);
    }
}