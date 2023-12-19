using Clones.Infrastructure;
using Clones.Types;
using UnityEngine;

namespace Clones.GameLogic
{
    public class PreyResourcesSpawner : EnviromentSpawner<PreyResourceType>
    {
        protected override void CreateEnviromentPart(IPartsFactory partsFactory, PreyResourceType enviromentType, Vector3 position, int rotation) => 
            partsFactory.CreatePreyResource(enviromentType, position, Quaternion.Euler(0, rotation, 0), transform);
    }
}