using UnityEngine;
using Clones.Services;
using Clones.StaticData;
using Clones.Infrastructure;

namespace Clones.GameLogic
{
    public class BoostsSpawner : MonoBehaviour, ITimeScalable
    {
        private float _minRadius;
        private float _maxRadius;
        private float _cooldown;
        private Transform _player;
        private BoostType[] _spawnedBoosts;
        private IPartsFactory _partsFactory;

        private float _timeLeft;

        private float _timeScale = 1;

        private void Update()
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime * _timeScale;
            }
            else
            {
                _timeLeft = _cooldown;

                Spawn();
            }
        }

        public void Init(float minRadius, float maxRadius, float cooldown, Transform player, BoostType[] spawnedBoosts)
        {
            _minRadius = minRadius;
            _maxRadius = maxRadius;
            _cooldown = cooldown;
            _player = player;
            _spawnedBoosts = spawnedBoosts;
            
            _timeLeft = 0;
        }

        public void Init(IPartsFactory partsFactory)
        {
            _partsFactory = partsFactory;
        }

        public void ScaleTime(float scale) => 
            _timeScale = scale;

        private void Spawn()
        {
            Vector3 spawnPosition = GetSpawnPosition();
            BoostType spawnedBoost = GetBoostType();

            _partsFactory.CreateBoost(spawnedBoost, spawnPosition, Quaternion.identity, transform);
        }

        private BoostType GetBoostType() => 
            _spawnedBoosts[Random.Range(0, _spawnedBoosts.Length)];

        private Vector3 GetSpawnPosition()
        {
            float distance = Random.Range(_minRadius, _maxRadius + 1);
            Vector2 point = Random.insideUnitCircle.normalized * distance + new Vector2(_player.position.x, _player.position.z);

            return new Vector3(point.x, 0, point.y);
        }
    }
}
