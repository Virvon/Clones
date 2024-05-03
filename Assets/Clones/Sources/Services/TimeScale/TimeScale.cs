using System.Collections.Generic;

namespace Clones.Services
{
    public class TimeScale : ITimeScaler
    {
        private List<ITimeScalable> _scalables;

        public TimeScale()
        {
            _scalables = new();
        }

        public void Scaled(float scale)
        {
            scale = scale >= 0 ? scale : 0;

            foreach (var scalable in _scalables)
                scalable.ScaleTime(scale);
        }

        public void Add(ITimeScalable scalable) =>
            _scalables.Add(scalable);

        public void Clear() =>
            _scalables.Clear();
    }
}