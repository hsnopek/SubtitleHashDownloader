using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleDownloader.Data.Model
{
    public class Language
    {
        public string Name { get; set; }
        public string CountryCode { get; set; }

        public Language()
        {
        }

        public Language(string name, string countryCode)
        {
            this.Name = name;
            this.CountryCode = countryCode;
        }


        public static string GetLanguageCode(string language)
        {
            string selectedLanguage = "";

            switch (language)
            {
                case "English":
                    selectedLanguage = "eng";
                    break;
                case "Croatian":
                    selectedLanguage = "hrv";
                    break;
                default:
                    break;
            }

            return selectedLanguage;
        }

    }

}
