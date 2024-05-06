using System;
using System.Collections.Generic;
using System.Linq;

namespace Clones.Data
{
    [Serializable]
    public class AveragePlayTime
    {
        private const int MaxPlayTimesCount = 7;

        public List<int> PlayTimes;

        public AveragePlayTime() => 
            PlayTimes = new();

        public void Add(int playTime)
        {
            if (playTime == 0)
                return;

            PlayTimes.Add(playTime);

            if(PlayTimes.Count > MaxPlayTimesCount)
                PlayTimes.RemoveAt(0);
        }

        public bool TryGetAveragePlayTime(out int averagePlayTime)
        {
            averagePlayTime = 0;

            if (PlayTimes.Count == 0)
                return false;

            averagePlayTime = PlayTimes.Sum() / PlayTimes.Count();

            return true;
        }
    }
}