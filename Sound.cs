using System.Collections.Generic;
using System.IO;

namespace launchsound
{
    public static class Sound
    {
        private static readonly string LOCATION = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "sounds");
        private static readonly Dictionary<int, string> SOUNDS = new Dictionary<int, string>
        {
            {0x7C, "pas faux.wav" },
            {0x7D, "respirer compote.wav" },
            {0x7E, "merde.wav" },
            {0x7F, "dire des trucs.wav" },
            {0x80, "fort ce con.wav" },
            {0x81, "cuillere.wav" },
            {0x82, "vous etes doué.wav" },
        };

        public static string GetSoundPath(int lintParam)
        {
            if (SOUNDS.ContainsKey(lintParam))
                return Path.Combine(LOCATION, SOUNDS[lintParam]);
            else
                return null;
        }

        public static string GetSoundName(int lintParam)
        {
            if (SOUNDS.ContainsKey(lintParam))
                return SOUNDS[lintParam];
            else
                return null;
        }
    }
}
