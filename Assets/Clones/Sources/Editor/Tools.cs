using Clones.Services;
using UnityEditor;
using UnityEngine;

namespace Clones.Editor
{
    internal class Tools
    {
        [MenuItem("Tools/SavePrefs")]
        public static void SavePrefs()
        {
            AllServices.Instance.Single<ISaveLoadService>().SaveProgress();
            PlayerPrefs.Save();
        }

        [MenuItem("Tools/ClearPrefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
