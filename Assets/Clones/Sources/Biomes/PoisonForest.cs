using System.Collections;
using UnityEngine;

namespace Clones.Biomes
{
    public class PoisonForest : Biome
    {
        [SerializeField, Range(0, 100)] private float _damagePercentage;
        [SerializeField] private float _coolDown;
        private void OnEnable() => 
            PlayerEntered += OnPlayerEntered;

        private void OnDisable() => 
            PlayerEntered -= OnPlayerEntered;

        private void OnPlayerEntered(Biome biome) => 
            StartCoroutine(Poisoning(Player.GetComponent<PlayerHealth>()));

        private IEnumerator Poisoning(PlayerHealth health)
        {
            bool isFirstAttack = true;

            while (Player != null)
            {
                if (isFirstAttack)
                    isFirstAttack = false;
                else
                    health.TakeDamage(health.MaxHealth * (_damagePercentage / 100));

                yield return new WaitForSeconds(_coolDown);
            }

            yield return null;
        }
    }
}
