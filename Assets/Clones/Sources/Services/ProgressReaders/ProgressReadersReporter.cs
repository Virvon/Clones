using System.Collections.Generic;

namespace Clones.Services
{
    public class ProgressReadersReporter : IProgressReadersReporter
    {
        private List<IProgressReader> _progressReaders;

        public ProgressReadersReporter() =>
            _progressReaders = new();

        public void Report()
        {
            foreach (IProgressReader progressReader in _progressReaders)
                progressReader.UpdateProgress();
        }

        public void Register(IProgressReader progressReader) =>
            _progressReaders.Add(progressReader);

        public void Clear() => 
            _progressReaders.Clear();
    }
}
