using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using StackRunner.Interactable;

namespace StackRunner.Managers
{
    /// <summary>
    ///  Summon and remove objects from this class instead of instantiating and destroying them.
    /// </summary>
    public class PoolManager : Singleton<PoolManager>
    {
        [SerializeField] private ParticleSystem deathParticle;


        private Queue<ParticleSystem> _particleQueue = new();


        private ObjectPool<ParticleSystem> _particlePool;


        private void Start()
        {
            InitializePools();
        }
        #region Pool Initializers
        private void InitializePools()
        {
            _particlePool = InitializeParticlePool(deathParticle);


        }

        private ObjectPool<ParticleSystem> InitializeParticlePool(ParticleSystem obj)
        {
            return new ObjectPool<ParticleSystem>(() =>
            {
                return Instantiate(obj);
            }, obj =>
            {
                obj.gameObject.SetActive(true);
            }, obj =>
            {
                obj.gameObject.SetActive(false);
            }, obj =>
            {
                Destroy(obj.gameObject);
            });
        }
        #endregion

        #region Pool Get and Release Methods
        /// <summary>
        ///  Summon a particle from pool.
        /// </summary>
        public void SpawnParticle(Vector3 particleLocation)
        {
            StartCoroutine(Co_ParticleSpawner(particleLocation));
        }
        private IEnumerator Co_ParticleSpawner(Vector3 particleLocation)
        {
            var particle = _particlePool.Get();
            particle.transform.position = particleLocation;
            _particleQueue.Enqueue(particle);
            yield return new WaitForSeconds(1);
            _particlePool.Release(_particleQueue.Peek());
            _particleQueue.Dequeue();
        }
        #endregion
    }
}