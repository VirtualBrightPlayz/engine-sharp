using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;

namespace GameSrc
{
    public class GameData
    {
        public string GameDir = "SCPCB";
        public string DataDir => Path.Combine(GameDir, "Data");
        public string LoadingScreensDir => Path.Combine(GameDir, "Loadingscreens");
        public string GFXDir => Path.Combine(GameDir, "GFX");
        public string MapDir => Path.Combine(GFXDir, "map");
        public string PropsDir => Path.Combine(MapDir, "Props");
        public string SFXDir => Path.Combine(GameDir, "SFX");
        public string RoomsFile => Path.Combine(DataDir, "rooms.ini");
        public string MaterialsFile => Path.Combine(DataDir, "materials.ini");
        public string LoadingScreensFile => Path.Combine(LoadingScreensDir, "loadingscreens.ini");
        private FileIniDataParser _parser;
        public IniData RoomsIni { get; private set; }
        public IniData MaterialsIni { get; private set; }
        public IniData LoadingScreensIni { get; private set; }

        public struct LoadingScreen
        {
            public string Name;
            public string ImagePath;
            public int AlignX;
            public int AlignY;
            public bool DisableBackground;
            public string[] Text;
        }

        public GameData() : this("SCPCB")
        {
        }

        public GameData(string path)
        {
            GameDir = path;
            _parser = new FileIniDataParser();
            RoomsIni = _parser.ReadFile(RoomsFile);
            MaterialsIni = _parser.ReadFile(MaterialsFile);
            LoadingScreensIni = _parser.ReadFile(LoadingScreensFile);
        }

        private static string Shape(MapGenerator.RoomType type)
        {
            switch (type)
            {
                case MapGenerator.RoomType.ROOM1:
                    return "1";
                case MapGenerator.RoomType.ROOM2:
                    return "2";
                case MapGenerator.RoomType.ROOM2C:
                    return "2C";
                case MapGenerator.RoomType.ROOM3:
                    return "3";
                case MapGenerator.RoomType.ROOM4:
                    return "4";
                default:
                    return string.Empty;
            }
        }

        public LoadingScreen[] GetLoadingScreens()
        {
            LoadingScreen[] screens = new LoadingScreen[LoadingScreensIni.Sections.Count];
            int i = 0;
            foreach (var section in LoadingScreensIni.Sections)
            {
                screens[i].Name = section.SectionName;
                screens[i].ImagePath = section.Keys["image path"];
                switch (section.Keys["align x"])
                {
                    default:
                        screens[i].AlignX = 0; // center
                        break;
                    case "left":
                        screens[i].AlignX = 2;
                        break;
                    case "right":
                        screens[i].AlignX = 1;
                        break;
                }
                switch (section.Keys["align y"])
                {
                    default:
                        screens[i].AlignY = 0; // center
                        break;
                    case "top":
                        screens[i].AlignY = 2;
                        break;
                    case "bottom":
                        screens[i].AlignY = 1;
                        break;
                }
                screens[i].DisableBackground = section.Keys.ContainsKey("disablebackground") && section.Keys["disablebackground"].Equals("true");
                List<string> texts = new List<string>();
                for (int j = 1; j <= 4; j++)
                {
                    if (!section.Keys.ContainsKey($"text{j}"))
                        break;
                    texts.Add(section.Keys[$"text{j}"]);
                }
                screens[i].Text = texts.ToArray();
                i++;
            }
            return screens;
        }

        public string GetBumpPath(string name)
        {
            if (MaterialsIni.Sections.ContainsSection(name) && MaterialsIni.Sections[name].ContainsKey("bump"))
            {
                return MaterialsIni.Sections[name]["bump"];
            }
            return null;
        }

        public string GetFloorType(string name)
        {
            if (MaterialsIni.Sections.ContainsSection(name) && MaterialsIni.Sections[name].ContainsKey("stepsound"))
            {
                return MaterialsIni.Sections[name]["stepsound"];
            }
            return string.Empty;
        }

        public string GetRoomPath(string name, MapGenerator.RoomType type, int zone, System.Random rng)
        {
            List<string> names = new List<string>();
            foreach (var item in RoomsIni.Sections)
            {
                bool inzone = false;
                foreach (var key in item.Keys)
                {
                    if (key.KeyName.StartsWith("zone") && key.Value == zone.ToString())
                    {
                        inzone = true;
                        break;
                    }
                }
                if (item.Keys.ContainsKey("mesh path") && item.Keys.ContainsKey("shape") && item.Keys.ContainsKey("commonness") && inzone)
                {
                    string key = item.Keys["mesh path"].Replace("\\", "/");
                    string shape = item.Keys["shape"];
                    if (shape.ToUpper() == Shape(type))
                    {
                        for (int i = 0; i < int.Parse(item.Keys["commonness"]); i++)
                        {
                            names.Add(item.SectionName);
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(name) && names.Count > 0)
            {
                name = names[rng.Next(0, names.Count)];
            }
            if (string.IsNullOrEmpty(name) || !RoomsIni.Sections.ContainsSection(name))
            {
                Console.WriteLine(type);
                Console.WriteLine(zone);
                return null;
            }
            return Path.Combine(GameDir, RoomsIni[name]["mesh path"].Replace("\\", "/"));
        }
    }
}