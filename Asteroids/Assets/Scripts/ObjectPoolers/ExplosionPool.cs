using Entities;
using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPoolers
{
    public sealed class ExplosionPool : MonoBehaviour
    {
        public static ExplosionPool Instance { get; private set; }
        
        [SerializeField] private ParticleSystem _explosion;

        private ObjectPool<ParticleSystem> _pool;

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
            _pool = new ObjectPool<ParticleSystem>(() =>
                    Instantiate(_explosion, gameObject.transform),
                prefab => { prefab.gameObject.SetActive(true); },
                prefab => { prefab.gameObject.SetActive(false); },
                prefab => { Destroy(prefab.gameObject); }, 
                false, 100, 200);
        }

        public ParticleSystem GetPrefab() => _pool.Get();

        public void ReleasePrefab(ParticleSystem particleSystem) => _pool.Release(particleSystem);
    }
}