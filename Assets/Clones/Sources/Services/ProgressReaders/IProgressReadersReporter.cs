using Clones.Data;

namespace Clones.Services
{
    public interface IProgressReadersReporter : IService
    {
        void Clear();
        void Register(IProgressReader progressReader);
        void Report();
    }
}