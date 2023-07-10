using System;
using UnityEngine;

namespace Clones.Progression
{
    public class WeightProgression
    {
        public float GetMaxWeight(int wave, float baseWeight)
        {
            return (float)(baseWeight * Mathf.Exp(wave / 10f));
        }
    }
}
