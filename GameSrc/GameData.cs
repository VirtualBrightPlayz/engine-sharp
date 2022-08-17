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
        public string GFXDir => Path.Combine(GameDir, "GFX");
        public string MapDir => Path.Combine(GFXDir, "map");
        public string PropsDir => Path.Combine(MapDir, "Props");
        public string SFXDir => Path.Combine(GameDir, "SFX");
        public string RoomsFile => Path.Combine(DataDir, "rooms.ini");
        public string MaterialsFile => Path.Combine(DataDir, "materials.ini");
        private FileIniDataParser _parser;
        public IniData RoomsIni { get; private set; }
        public IniData MaterialsIni { get; private set; }

        public GameData() : this("SCPCB")
        {
        }

        public GameData(string path)
        {
            GameDir = path;
            _parser = new FileIniDataParser();
            RoomsIni = _parser.ReadFile(RoomsFile);
            MaterialsIni = _parser.ReadFile(MaterialsFile);
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

        public string GetBumpPath(string name)
        {
            if (MaterialsIni.Sections.ContainsSection(name) && MaterialsIni.Sections[name].ContainsKey("bump"))
            {
                return MaterialsIni.Sections[name]["bump"];
            }
            return null;
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
            return Path.Combine(GameDir, RoomsIni[name]["mesh path"].Replace("\\", "/"));//ResourceManagerExtensions.LoadRoomModel(Path.Combine(GameDir, roomsData[name]["mesh path"].Replace("\\", "/")));
        }
    }
}