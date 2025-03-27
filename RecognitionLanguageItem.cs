using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranscribeAudioWpfApp
{
    class RecognitionLanguageItem
    {
        public string DisplayName { get; set; }
        public string LanguageCode { get; set; }
        public RecognitionLanguageItem(string displayName, string languageCode)
        {
            DisplayName = displayName;
            LanguageCode = languageCode;
        }
        public override string ToString()
        {
            return DisplayName;
        }
    }
}
