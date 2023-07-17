using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryCharacters : MonoBehaviour
{
    private Dictionary<string, CharacterStats> _clones = new Dictionary<string, CharacterStats>();

    private void Awake()
    {
        if(_clones.Count == 0)
            Initialize();
    }

    private void Initialize()
    {
        _clones.Add("Normal", new CharacterStats(100, 15, 1f, 1f, 25, 25));
        _clones.Add("Rare", new CharacterStats(130, 20, 1f, 1.5f));
        _clones.Add("Epic", new CharacterStats(180, 25, 1f, 2.5f));
        _clones.Add("Legendari", new CharacterStats(250, 30, 1f, 4f));
    }
}
