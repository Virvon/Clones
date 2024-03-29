using Clones.Types;
using System;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New PreyResource", menuName = "Data/Create new prey resource", order = 51)]
    public class PreyResourceStaticData : ScriptableObject
    {
        public PreyResourceType Type;
        public QuestItemType DroppetItem;
        public PreyResource Prefab;
        public int HitsCountToDie;
        public GameObject DiedEffect;
        public Vector3 EffectOffset;
        public AudioClip DamageAudio;
        [Range(0, 1)] public float DamageAudioVolume;
        public AudioClip DieAudio;
        [Range(0, 1)] public float DieAudioVolume;
    }
}
