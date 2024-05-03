namespace Clones.Services
{
    public interface ITimeScaler : IService
    {
        void Scaled(float scale);
        void Add(ITimeScalable scalable);
        void Clear();
    }
}