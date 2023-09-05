using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DieReporter))]
public class PreyResourcesVault : MonoBehaviour
{
    [SerializeField] private WorldGenerator _worldGenerator;

    private DieReporter _dieReporter;
   
    private void OnEnable()
    {
        _dieReporter = GetComponent<DieReporter>();

        _worldGenerator.TilesGenerated += OnTilesGenerated;
        _worldGenerator.TilesDiactivated += OnTilesDeactivated;
    }

    private void OnDisable()
    {
        _worldGenerator.TilesGenerated -= OnTilesGenerated;
        _worldGenerator.TilesDiactivated -= OnTilesDeactivated;
    }

    private void OnTilesGenerated(IReadOnlyList<GeneratorObjects> generatorsObjects)
    {
        List<IDamageable> damageables = new List<IDamageable>();

        

        foreach(var generator in generatorsObjects)
        {
            foreach(var preyResource in generator.PreyResources)
            {
                damageables.Add(preyResource);
            }
        }

        _dieReporter.TakeIDamagebles(damageables);
    }

    private void OnTilesDeactivated(IReadOnlyList<GeneratorObjects> generatorsObjects)
    {
        List<IDamageable> damageables = new List<IDamageable>();

        foreach (var generator in generatorsObjects)
        {
            foreach (var preyResource in generator.PreyResources)
                damageables.Add(preyResource);
        }

        _dieReporter.DeactivateIDamagebles(damageables);
    }
}
