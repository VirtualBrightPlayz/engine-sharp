using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameBSrc
{
    public class GameData
    {
        public string GameDir = "087-B";
        public string GFXDir => Path.Combine("GFX");
        public string SFXDir => Path.Combine("SFX");

        public GameData() : this("087-B")
        {
        }

        public GameData(string path)
        {
            GameDir = path;
        }
    }
}