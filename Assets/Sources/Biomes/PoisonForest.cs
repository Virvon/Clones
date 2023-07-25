using System.Collections;
using UnityEngine;

namespace Clones.Biomes
{
    public class PoisonForest : Biome
    {
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
                    Player.TakeDamage(4);

                yield return new WaitForSeconds(2);
            }
        }
    }
}
