using Combat.Projectiles.Core;
using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPoolers
{
    public sealed class ProjectilePool : MonoBehaviour
    {
        public static ProjectilePool Instance { get; private set; }
        
        [SerializeField] private Projectile _projectilePrefab;

        private ObjectPool<Projectile> _pool;

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
            _pool = new ObjectPool<Projectile>(() =>
                    Instantiate(_projectilePrefab, gameObject.transform), 
                prefab => { prefab.gameObject.SetActive(true); },
                prefab => { prefab.gameObject.SetActive(false); },
                prefab => { Destroy(prefab.gameObject); },
                false, 100, 200);
        }


        public Projectile GetPrefab() => _pool.Get();

        public void ReleasePrefab(Projectile projectile) => _pool.Release(projectile);
    }
}