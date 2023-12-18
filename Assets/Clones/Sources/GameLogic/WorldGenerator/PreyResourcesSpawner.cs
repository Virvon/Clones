using Clones.Infrastructure;
using Clones.Types;
using UnityEngine;

namespace Clones.GameLogic
{
    public class PreyResourcesSpawner : EnviromentSpawner<PreyResourceType>
    {
        protected override void CreateEnviromentPart(IPartsFactory partsFactory, PreyResourceType enviromentType, Vector3 position) => 
            partsFactory.CreatePreyResource(enviromentType, position, Quaternion.identity, transform);
    }
}