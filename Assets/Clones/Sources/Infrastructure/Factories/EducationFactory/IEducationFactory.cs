using Clones.EducationLogic;
using Clones.Services;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IEducationFactory : IService
    {
        EducationPreyResourcesSpawner CreatePreyResourcesSpawner();
        EducationEnemiesSpawner CreateEnemiesSpawner(GameObject playerObject);
    }
}