using System.Collections;
using UnityEngine;

namespace Clones.Biomes
{
    public class PoisonForest : Biome
    {
        [SerializeField, Range(0, 100)] private float _damagePercentage;
        [SerializeField] private float _coolDown;
        private void OnEnable() => PlayerEntered += OnPlayerEntered;

        private void OnDisable() => PlayerEntered -= OnPlayerEntered;

        private void OnPlayerEntered(Biome biome) => StartCoroutine(Poisoning());

        private IEnumerator Poisoning()
        {
            bool isFirstAttack = true;

            while (Player != null)
            {
                if (isFirstAttack)
                    isFirstAttack = false;
                else
                    Player.TakeDamage(Player.MaxHealth * (_damagePercentage / 100));

                yield return new WaitForSeconds(_coolDown);
            }
        }
    }
}
