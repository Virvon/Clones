using UnityEngine;

namespace Clones.SaveUtility
{
    public static class SaveUtility
    {
        public static void Save<T>(string key, T saveData)
        {
            string jsonData = JsonUtility.ToJson(saveData, true);
            PlayerPrefs.SetString(key, jsonData);
            PlayerPrefs.Save();
        }

        public static T Load<T>(string key) where T : new()
        {
            if (PlayerPrefs.HasKey(key))
            {
                var loadedString = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<T>(loadedString);
            }
            else
            {
                return new T();
            }
        }
    }
}
