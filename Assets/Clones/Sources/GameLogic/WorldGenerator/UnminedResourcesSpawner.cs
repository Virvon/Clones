using Clones.Infrastructure;
using Clones.Types;
using UnityEngine;

namespace Clones.GameLogic
{
    public class UnminedResourcesSpawner : EnviromentSpawner<UnminedResourceType>
    {
        protected override void CreateEnviromentPart(IPartsFactory partsFactory, UnminedResourceType enviromentType, Vector3 position, int rotation) => 
            partsFactory.CreateUnminedResource(enviromentType, position, Quaternion.Euler(0, rotation, 0), transform);
    }
}