using Clones.EducationLogic;
using Clones.Services;

namespace Clones.Infrastructure
{
    public interface IEducationFactory : IService
    {
        EducationPreyResourcesSpawner CreateSpawner();
    }
}