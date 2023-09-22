namespace Clones.Services
{
    public interface ITimeScale : IService
    {
        void Scaled(float scale);
        void Add(ITimeScalable scalable);
        void Clear();
    }
}