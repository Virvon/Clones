using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;

namespace Clones.Services
{
    public class Localization : ILocalization
    {
        private const string DefaultIsoLanguage = "en";

        private readonly Dictionary<string, string> _languages = new Dictionary<string, string>
        {
            {DefaultIsoLanguage, "English" },
            {"ru", "Russian" },
            {"tr", "Turkish" }
        };

        private string _currentIsoLanguage;

        public Localization(ICoroutineRunner coroutineRunner) => 
            coroutineRunner.StartCoroutine(SetCurrentLanguage());

        public string GetLeanLanguage() =>
            _languages.TryGetValue(_currentIsoLanguage, out string leanLanguage) ? leanLanguage : _languages[DefaultIsoLanguage];

        public string GetIsoLanguage() =>
            _currentIsoLanguage;

        private IEnumerator SetCurrentLanguage()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            _currentIsoLanguage = "ru";
                yield break;
#else
            while (YandexGamesSdk.IsInitialized == false)
                yield return null;

            _currentIsoLanguage = YandexGamesSdk.Environment.i18n.lang;
#endif
        }
    }
}
