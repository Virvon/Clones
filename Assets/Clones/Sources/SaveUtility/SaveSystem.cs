using UnityEngine;

namespace Clones.SaveUtility
{
    public static class SaveSystem
    {
        public static void Save<T>(string key, T saveProfile)
        {
            string jsonProfile = JsonUtility.ToJson(saveProfile, true);

            PlayerPrefs.SetString(key, jsonProfile);
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
