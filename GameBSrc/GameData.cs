using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;

namespace GameBSrc
{
    public class GameData
    {
        public string GameDir = "087-B";
        public string GFXDir => Path.Combine(GameDir, "GFX");
        public string SFXDir => Path.Combine(GameDir, "SFX");
        private FileIniDataParser _parser;
        public IniData RoomsIni { get; private set; }
        public IniData MaterialsIni { get; private set; }

        public GameData() : this("087-B")
        {
        }

        public GameData(string path)
        {
            GameDir = path;
            _parser = new FileIniDataParser();
        }
    }
}