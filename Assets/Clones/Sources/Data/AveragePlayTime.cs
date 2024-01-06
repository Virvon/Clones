using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            PlayTimes.Add(playTime);

            if(PlayTimes.Count > MaxPlayTimesCount)
                PlayTimes.RemoveAt(0);

            Debug.Log(PlayTimes.Count());
            Debug.Log(GetAveragePlayTime());
        }

        public int GetAveragePlayTime() => 
            PlayTimes.Sum() / PlayTimes.Count();
    }
}