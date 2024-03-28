using System;
using System.Collections.Generic;

namespace Clones.Data
{
    [Serializable]
    public class Language
    {
        private const string DefaultIsoLanguage = "en";

        private readonly Dictionary<string, string> Languages = new Dictionary<string, string>
        {
            {"en", "English" },
            {"ru", "Russian" },
            {"tr", "Turkish" }
        };

        public string CurrentIsoLanguage;

        public bool TryGetCurrentLeanLanguage(out string language)
        {
            language = null;

            if (CurrentIsoLanguage != null && Languages.TryGetValue(CurrentIsoLanguage, out language))
                return true;
            else
                return false;
        }

        public string TranslateToLeanLanguage(string isoLanguage) => 
            Languages.TryGetValue(isoLanguage, out string leanLanguage) ? leanLanguage : DefaultIsoLanguage;
    }
}