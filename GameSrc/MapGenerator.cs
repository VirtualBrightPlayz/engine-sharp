using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Engine.Game.Entities;

namespace GameSrc
{
    public class MapGenerator
    {
        public GameData data;
        public int[] Transitions = new int[] { 13, 7 };
        public int RandomSeed;
        public List<Entity> UnspawnedEntities = new List<Entity>();
        public Vector2Int MapSize = new Vector2Int()
        {
            x = 18,
            y = 18,
        };
        public enum RoomType : int
        {
            ROOM1,
            ROOM2,
            ROOM2C,
            ROOM3,
            ROOM4,
        }
        public struct Vector2Int
        {
            public int x, y;
        }

        public int GetZone(int y)
        {
            int zone = 1;
            if (y >= Transitions[1] && y < Transitions[0])
            {
                zone = 2;
            }
            if (y >= Transitions[0])
            {
                zone = 3;
            }
            return zone;
        }

        public Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            return Quaternion.CreateFromYawPitchRoll((pitch-180f) * (MathF.PI / 180f), yaw * (MathF.PI / 180f), roll * (MathF.PI / 180f));
        }

        public bool IsInBounds(int[,] arr, int d1)
        {
            return d1 >= 0 && d1 < arr.GetLength(0);
        }

        public IEnumerator CreateMap(IProgress<int> progress, CancellationTokenSource token)
        {
            UnspawnedEntities.Clear();
            System.Random rng = new System.Random(RandomSeed);
            string[,] MapName = new string[MapSize.x, MapSize.y];
            int[,] MapTemp = new int[MapSize.x, MapSize.y];
            int[] MapRoomId = new int[5];
            int x = MapSize.x / 2;
            int y = MapSize.y - 2;
            int temp = 0;

            for (int i = y; i < MapSize.y; i++)
            {
                MapTemp[x, i] = 1;
            }

            while (y >= 2)
            {
                int width = rng.Next(10, 16);

                if (x > MapSize.x * 0.6f)
                {
                    width = -width;
                }
                else if (x > MapSize.x * 0.4f)
                {
                    x = x - width / 2;
                }

                if (x + width > MapSize.x - 3)
                {
                    width = MapSize.x - 3 - x;
                }
                else if (x + width < 2)
                {
                    width = -x + 2;
                }

                x = Math.Min(x, x + width);
                width = Math.Abs(width);
                for (int i = x; i < x + width; i++)
                {
                    MapTemp[Math.Min(i, (int)MapSize.x), y] = 1;
                }

                int height = rng.Next(3, 5);

                if (y - height < 1)
                    height = y - 1;
                
                int yhallways = rng.Next(4, 6);

                if (GetZone(y - height) != GetZone(y - height - 1))
                    height--;
                
                for (int i = 0; i < yhallways; i++)
                {
                    int x2 = Math.Max(Math.Min(rng.Next(x, x + width), (int)MapSize.x - 2), 2);
                    while (IsInBounds(MapTemp, x2) && IsInBounds(MapTemp, x2-1) && IsInBounds(MapTemp, x2+1) && (MapTemp[x2, y-1] >= 1 || MapTemp[x2-1, y-1] >= 1 || MapTemp[x2+1, y-1] >= 1))
                    {
                        x2++;
                    }

                    if (x2 < x + width)
                    {
                        int tempheight;
                        if (i == 0)
                        {
                            tempheight = height;
                            if (rng.Next(1, 3) == 1)
                                x2 = x;
                            else
                                x2 = x + width;
                        }
                        else
                        {
                            tempheight = rng.Next(1, height + 1);
                        }

                        for (int y2 = y - tempheight; y2 < y; y2++)
                        {
                            if (GetZone(y2) != GetZone(y2 + 1))
                                MapTemp[x2, y2] = 255;
                            else
                                MapTemp[x2, y2] = 1;
                        }

                        if (tempheight == height)
                        {
                            temp = x2;
                        }
                    }
                }

                x = temp;
                y = y - height;
            }
            
            for (int k = 0; k < MapSize.y; k++)
            {
                for (int j = 0; j < MapSize.x; j++)
                {
                    Vector3 pos = new Vector3(j, 0f, k);
                    // if (MapTemp[j, k] >= 1)
                        // Debug.DrawLine(pos, pos + Vector3.up, Color.red, 1f);
                }
            }

            int ZoneAmount = 3;
            int[] Room1Amount = new int[ZoneAmount];
            int[] Room2Amount = new int[ZoneAmount];
            int[] Room2CAmount = new int[ZoneAmount];
            int[] Room3Amount = new int[ZoneAmount];
            int[] Room4Amount = new int[ZoneAmount];

            for (y = 1; y < MapSize.y - 1; y++)
            {
                int zone = GetZone(y) - 1;

                for (x = 1; x < MapSize.x - 1; x++)
                {
                    if (MapTemp[x, y] > 0)
                    {
                        temp = Math.Min(MapTemp[x + 1, y], 1) + Math.Min(MapTemp[x - 1, y], 1);
                        temp = temp + Math.Min(MapTemp[x, y + 1], 1) + Math.Min(MapTemp[x, y - 1], 1);
                        if (MapTemp[x, y] < 255)
                            MapTemp[x, y] = temp;
                        switch (MapTemp[x, y])
                        {
                            case 1:
                                Room1Amount[zone]++;
                                break;
                            case 2:
                                if (Math.Min(MapTemp[x + 1, y], 1) + Math.Min(MapTemp[x - 1, y], 1) == 2)
                                {
                                    Room2Amount[zone]++;
                                }
                                else if (Math.Min(MapTemp[x, y + 1], 1) + Math.Min(MapTemp[x, y - 1], 1) == 2)
                                {
                                    Room2Amount[zone]++;
                                }
                                else
                                {
                                    Room2CAmount[zone]++;
                                }
                                break;
                            case 3:
                                Room3Amount[zone]++;
                                break;
                            case 4:
                                Room4Amount[zone]++;
                                break;
                        }
                    }
                }
            }

            // force more room1s (if needed)
            for (int i = 0; i < 3; i++)
            {
                temp = -Room1Amount[i]+5;

                if (temp > 0)
                {
                    for (y = (MapSize.y/ZoneAmount)*(2-i)+1; y < ((MapSize.y/ZoneAmount)*((2-i)+1.0f))-2; y++)
                    {
                        for (x = 2; x < MapSize.x - 2; x++)
                        {
                            if (MapTemp[x, y] == 0)
                            {
                                if ((Math.Min(MapTemp[x + 1, y],1) + Math.Min(MapTemp[x - 1, y],1) + Math.Min(MapTemp[x, y + 1],1) + Math.Min(MapTemp[x, y - 1],1)) == 1)
                                {
                                    int x2 = 0;
                                    int y2 = 0;
                                    if (MapTemp[x+1, y] != 0)
                                    {
                                        x2 = x+1;
                                        y2 = y;
                                    }
                                    else if (MapTemp[x-1, y] != 0)
                                    {
                                        x2 = x-1;
                                        y2 = y;
                                    }
                                    if (MapTemp[x, y+1] != 0)
                                    {
                                        x2 = x;
                                        y2 = y+1;
                                    }
                                    else if (MapTemp[x, y-1] != 0)
                                    {
                                        x2 = x;
                                        y2 = y-1;
                                    }

                                    bool placed = false;
                                    if (MapTemp[x2, y2] > 1 && MapTemp[x2, y2] < 4)
                                    {
                                        switch (MapTemp[x2, y2])
                                        {
                                            case 2:
                                                if (Math.Min(MapTemp[x2+1, y2], 1) + Math.Min(MapTemp[x2 - 1, y2], 1) == 2)
                                                {
                                                    Room2Amount[i]--;
                                                    Room3Amount[i]++;
                                                    placed = true;
                                                }
                                                else if (Math.Min(MapTemp[x2, y2+1], 1) + Math.Min(MapTemp[x2, y2 - 1], 1) == 2)
                                                {
                                                    Room2Amount[i]--;
                                                    Room3Amount[i]++;
                                                    placed = true;
                                                }
                                                break;
                                            case 3:
                                                Room3Amount[i]--;
                                                Room4Amount[i]++;
                                                placed = true;
                                                break;
                                        }

                                        if (placed)
                                        {
                                            MapTemp[x2, y2] = MapTemp[x2, y2] + 1;
                                            MapTemp[x, y] = 1;
                                            Room1Amount[i]++;

                                            temp--;
                                        }
                                    }
                                }
                            }
                            if (temp == 0)
                                break;
                        }
                        if (temp == 0)
                            break;
                    }
                }
            }

            // room4 and room2c
            for (int i = 0; i < 3; i++)
            {
                int zone = 1;
                int temp2 = 0;
                switch (i)
                {
                    case 2:
                        zone = 2;
                        temp2 = MapSize.y/3;
                        break;
                    case 1:
                        zone = MapSize.y/3+1;
                        temp2 = (int)Math.Floor(MapSize.y*(2f/3f)-1);
                        break;
                    case 0:
                        zone = (int)Math.Floor(MapSize.y*(2f/3f)+1);
                        temp2 = MapSize.y-2;
                        break;
                }

                if (Room4Amount[i]<1)
                {
                    temp = 0;

                    for (y = zone; y < temp2 + 1; y++)
                    {
                        for (x = 2; x < MapSize.x - 1; x++)
                        {
                            if (MapTemp[x, y] == 3)
                            {
                                if (MapTemp[x+1,y] > 0 || MapTemp[x+1,y+1] > 0 || MapTemp[x+1,y-1] > 0 || MapTemp[x+2,y] > 0)
                                {
                                    MapTemp[x+1,y] = 1;
                                    temp = 1;
                                }
                                else if (MapTemp[x-1,y] > 0 || MapTemp[x-1,y+1] > 0 || MapTemp[x-1,y-1] > 0 || MapTemp[x-2,y] > 0)
                                {
                                    MapTemp[x-1,y] = 1;
                                    temp = 1;
                                }
                                else if (MapTemp[x,y+1] > 0 || MapTemp[x+1,y+1] > 0 || MapTemp[x-1,y+1] > 0 || MapTemp[x,y+2] > 0)
                                {
                                    MapTemp[x,y+1] = 1;
                                    temp = 1;
                                }
                                else if (MapTemp[x,y-1] > 0 || MapTemp[x+1,y-1] > 0 || MapTemp[x-1,y-1] > 0 || MapTemp[x,y-2] > 0)
                                {
                                    MapTemp[x,y-1] = 1;
                                    temp = 1;
                                }

                                if (temp == 1)
                                {
                                    MapTemp[x,y] = 4;
                                    Room4Amount[i]++;
                                    Room3Amount[i]--;
                                    Room1Amount[i]++;
                                }
                            }
                            if (temp == 1)
                                break;
                        }
                        if (temp == 1)
                            break;
                    }

                    if (temp == 0)
                    {
                        Log.Debug(nameof(MapGenerator), $"ROOM4 not placed in zone {i}");
                    }
                }

                if (Room2CAmount[i] < 1)
                {
                    temp = 0;

                    zone++;
                    temp2--;

                    for (y = zone; y < temp2 + 1; y++)
                    {
                        for (x = 3; x < MapSize.x - 3 + 1; x++)
                        {
                            if (MapTemp[x,y] == 1)
                            {
                                if (MapTemp[x-1,y] > 0)
                                {
                                    if (MapTemp[x,y-1]+MapTemp[x,y+1]+MapTemp[x+2,y] == 0)
                                    {
                                        if (MapTemp[x+1,y-2]+MapTemp[x+2,y-1]+MapTemp[x+1,y-1] == 0)
                                        {
                                            MapTemp[x,y] = 2;
                                            MapTemp[x+1,y] = 2;
                                            MapTemp[x+1,y-1] = 1;
                                            temp = 1;
                                        }
                                        else if (MapTemp[x+1,y+2]+MapTemp[x+2,y+1]+MapTemp[x+1,y+1] == 0)
                                        {
                                            MapTemp[x,y] = 2;
                                            MapTemp[x+1,y] = 2;
                                            MapTemp[x+1,y+1] = 1;
                                            temp = 1;
                                        }
                                    }
                                }
                                else if (MapTemp[x+1,y] > 0)
                                {
                                    if (MapTemp[x,y-1]+MapTemp[x,y+1]+MapTemp[x-2,y] == 0)
                                    {
                                        if (MapTemp[x-1,y-2]+MapTemp[x-2,y-1]+MapTemp[x-1,y-1] == 0)
                                        {
                                            MapTemp[x,y] = 2;
                                            MapTemp[x-1,y] = 2;
                                            MapTemp[x-1,y-1] = 1;
                                            temp = 1;
                                        }
                                        else if (MapTemp[x-1,y+2]+MapTemp[x-2,y+1]+MapTemp[x-1,y+1] == 0)
                                        {
                                            MapTemp[x,y] = 2;
                                            MapTemp[x-1,y] = 2;
                                            MapTemp[x-1,y+1] = 1;
                                            temp = 1;
                                        }
                                    }
                                }
                                else if (MapTemp[x,y-1] > 0)
                                {
                                    if (MapTemp[x-1,y]+MapTemp[x+1,y]+MapTemp[x,y+2] == 0)
                                    {
                                        if (MapTemp[x-2,y+1]+MapTemp[x-1,y+2]+MapTemp[x-1,y+1] == 0)
                                        {
                                            MapTemp[x,y] = 2;
                                            MapTemp[x,y+1] = 2;
                                            MapTemp[x-1,y+1] = 1;
                                            temp = 1;
                                        }
                                        else if (MapTemp[x+2,y+1]+MapTemp[x+1,y+2]+MapTemp[x+1,y+1] == 0)
                                        {
                                            MapTemp[x,y] = 2;
                                            MapTemp[x,y+1] = 2;
                                            MapTemp[x+1,y+1] = 1;
                                            temp = 1;
                                        }
                                    }
                                }
                                else if (MapTemp[x,y+1] > 0)
                                {
                                    if (MapTemp[x-1,y]+MapTemp[x+1,y]+MapTemp[x,y-2] == 0)
                                    {
                                        if (MapTemp[x-2,y-1]+MapTemp[x-1,y-2]+MapTemp[x-1,y-1] == 0)
                                        {
                                            MapTemp[x,y] = 2;
                                            MapTemp[x,y-1] = 2;
                                            MapTemp[x-1,y-1] = 1;
                                            temp = 1;
                                        }
                                        else if (MapTemp[x+2,y-1]+MapTemp[x+1,y-2]+MapTemp[x+1,y-1] == 0)
                                        {
                                            MapTemp[x,y] = 2;
                                            MapTemp[x,y-1] = 2;
                                            MapTemp[x+1,y-1] = 1;
                                            temp = 1;
                                        }
                                    }
                                }

                                if (temp == 1)
                                {
                                    Room2CAmount[i]++;
                                    Room2Amount[i]++;
                                }
                            }
                            if (temp == 1)
                                break;
                        }
                        if (temp == 1)
                            break;
                    }

                    if (temp == 0)
                    {
                        Log.Debug(nameof(MapGenerator), $"ROOM2C not placed in zone {i}");
                    }
                }
            }

            int MaxRooms = 55 * MapSize.x / 20;
            MaxRooms = Math.Max(MaxRooms, Room1Amount[0]+Room1Amount[1]+Room1Amount[2]+1);
            MaxRooms = Math.Max(MaxRooms, Room2Amount[0]+Room2Amount[1]+Room2Amount[2]+1);
            MaxRooms = Math.Max(MaxRooms, Room2CAmount[0]+Room2CAmount[1]+Room2CAmount[2]+1);
            MaxRooms = Math.Max(MaxRooms, Room3Amount[0]+Room3Amount[1]+Room3Amount[2]+1);
            MaxRooms = Math.Max(MaxRooms, Room4Amount[0]+Room4Amount[1]+Room4Amount[2]+1);
            string[,] MapRoom = new string[(int)RoomType.ROOM4 + 1, MaxRooms];

            // zone 1
            int min_pos = 1;
            int max_pos = Room1Amount[0]-1;

            MapRoom[(int)RoomType.ROOM1, 0] = "start";
            SetRoom(ref MapRoom, "roompj", RoomType.ROOM1, (int)Math.Floor(0.1f * Room1Amount[0]), min_pos, max_pos);
            SetRoom(ref MapRoom, "914", RoomType.ROOM1, (int)Math.Floor(0.3f * Room1Amount[0]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room1archive", RoomType.ROOM1, (int)Math.Floor(0.5f * Room1Amount[0]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room205", RoomType.ROOM1, (int)Math.Floor(0.6f * Room1Amount[0]), min_pos, max_pos);

            MapRoom[(int)RoomType.ROOM2, 0] = "lockroom";

            min_pos = 1;
            max_pos = Room2Amount[0]-1;

            MapRoom[(int)RoomType.ROOM2, 0] = "room2closets";
            SetRoom(ref MapRoom, "room2testroom2", RoomType.ROOM2, (int)Math.Floor(0.1f * Room2Amount[0]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2scps", RoomType.ROOM2, (int)Math.Floor(0.2f * Room2Amount[0]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2storage", RoomType.ROOM2, (int)Math.Floor(0.3f * Room2Amount[0]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2gw_b", RoomType.ROOM2, (int)Math.Floor(0.4f * Room2Amount[0]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2sl", RoomType.ROOM2, (int)Math.Floor(0.5f * Room2Amount[0]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room012", RoomType.ROOM2, (int)Math.Floor(0.55f * Room2Amount[0]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2scps2", RoomType.ROOM2, (int)Math.Floor(0.6f * Room2Amount[0]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room1123", RoomType.ROOM2, (int)Math.Floor(0.7f * Room2Amount[0]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2elevator", RoomType.ROOM2, (int)Math.Floor(0.85f * Room2Amount[0]), min_pos, max_pos);

            MapRoom[(int)RoomType.ROOM3, (int)Math.Floor(rng.Next(20, 80) / 100f * Room3Amount[0])] = "room3storage";
            MapRoom[(int)RoomType.ROOM2C, (int)Math.Floor(0.5f*Room2CAmount[0])] = "room1162";
            MapRoom[(int)RoomType.ROOM4, (int)Math.Floor(0.3f*Room4Amount[0])] = "room4info";

            // zone 2
            min_pos = Room1Amount[0];
            max_pos = Room1Amount[0]+Room1Amount[1]-1;

            SetRoom(ref MapRoom, "room079", RoomType.ROOM1, Room1Amount[0]+(int)Math.Floor(0.15f*Room1Amount[1]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room106", RoomType.ROOM1, Room1Amount[0]+(int)Math.Floor(0.3f*Room1Amount[1]), min_pos, max_pos);
            SetRoom(ref MapRoom, "008", RoomType.ROOM1, Room1Amount[0]+(int)Math.Floor(0.4f*Room1Amount[1]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room035", RoomType.ROOM1, Room1Amount[0]+(int)Math.Floor(0.5f*Room1Amount[1]), min_pos, max_pos);
            SetRoom(ref MapRoom, "coffin", RoomType.ROOM1, Room1Amount[0]+(int)Math.Floor(0.7f*Room1Amount[1]), min_pos, max_pos);

            min_pos = Room2Amount[0];
            max_pos = Room2Amount[0]+Room2Amount[1]-1;

            MapRoom[(int)RoomType.ROOM2, Room2Amount[0]+(int)Math.Floor(0.1f*Room2Amount[1])] = "room2nuke";
            SetRoom(ref MapRoom, "room2tunnel", RoomType.ROOM2, Room2Amount[0]+(int)Math.Floor(0.25f*Room2Amount[1]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room049", RoomType.ROOM2, Room2Amount[0]+(int)Math.Floor(0.4f*Room2Amount[1]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2shaft", RoomType.ROOM2, Room2Amount[0]+(int)Math.Floor(0.6f*Room2Amount[1]), min_pos, max_pos);
            SetRoom(ref MapRoom, "testroom", RoomType.ROOM2, Room2Amount[0]+(int)Math.Floor(0.7f*Room2Amount[1]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2servers", RoomType.ROOM2, Room2Amount[0]+(int)Math.Floor(0.9f*Room2Amount[1]), min_pos, max_pos);

            MapRoom[(int)RoomType.ROOM3, Room3Amount[0]+(int)Math.Floor(0.3f*Room3Amount[1])] = "room513";
            MapRoom[(int)RoomType.ROOM3, Room3Amount[0]+(int)Math.Floor(0.6f*Room3Amount[1])] = "room966";

            MapRoom[(int)RoomType.ROOM2C, Room2Amount[0]+(int)Math.Floor(0.5f*Room2Amount[1])] = "room2cpit";

            // zone 3
            MapRoom[(int)RoomType.ROOM1, Room1Amount[0]+Room1Amount[1]+Room1Amount[2]-2] = "exit1";
            MapRoom[(int)RoomType.ROOM1, Room1Amount[0]+Room1Amount[1]+Room1Amount[2]-1] = "gateaentrance";
            MapRoom[(int)RoomType.ROOM1, Room1Amount[0]+Room1Amount[1]] = "room1lifts";

            min_pos = Room2Amount[0]+Room2Amount[1];
            max_pos = Room2Amount[0]+Room2Amount[1]+Room2Amount[2]-1;

            MapRoom[(int)RoomType.ROOM2, min_pos+(int)Math.Floor(0.1f*Room2Amount[2])] = "room2poffices";
            SetRoom(ref MapRoom, "room2cafeteria", RoomType.ROOM2, min_pos+(int)Math.Floor(0.2f*Room2Amount[2]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2sroom", RoomType.ROOM2, min_pos+(int)Math.Floor(0.3f*Room2Amount[2]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2servers2", RoomType.ROOM2, min_pos+(int)Math.Floor(0.4f*Room2Amount[2]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2offices", RoomType.ROOM2, min_pos+(int)Math.Floor(0.45f*Room2Amount[2]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2offices4", RoomType.ROOM2, min_pos+(int)Math.Floor(0.5f*Room2Amount[2]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room860", RoomType.ROOM2, min_pos+(int)Math.Floor(0.6f*Room2Amount[2]), min_pos, max_pos);
            SetRoom(ref MapRoom, "medibay", RoomType.ROOM2, min_pos+(int)Math.Floor(0.7f*Room2Amount[2]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2poffices2", RoomType.ROOM2, min_pos+(int)Math.Floor(0.8f*Room2Amount[2]), min_pos, max_pos);
            SetRoom(ref MapRoom, "room2offices2", RoomType.ROOM2, min_pos+(int)Math.Floor(0.8f*Room2Amount[2]), min_pos, max_pos);

            MapRoom[(int)RoomType.ROOM2C, Room2CAmount[0]+Room2CAmount[1]] = "room2ccont";
            MapRoom[(int)RoomType.ROOM2C, Room2CAmount[0]+Room2CAmount[1]+1] = "lockroom2";

            MapRoom[(int)RoomType.ROOM3, Room3Amount[0]+Room3Amount[1]+(int)Math.Floor(0.3f*Room3Amount[2])] = "room3servers";
            MapRoom[(int)RoomType.ROOM3, Room3Amount[0]+Room3Amount[1]+(int)Math.Floor(0.7f*Room3Amount[2])] = "room3servers2";
            // MapRoom[(int)RoomType.ROOM3, Room3Amount[0]+Room3Amount[1]] = "room3gw";
            MapRoom[(int)RoomType.ROOM3, Room3Amount[0]+Room3Amount[1]+(int)Math.Floor(0.5f*Room3Amount[2])] = "room3offices";

            yield return SpawnMap(MapTemp, MapRoom, MaxRooms, rng, progress, token);
        }

        public IEnumerator SpawnMap(int[,] MapTemp, string[,] MapRoom, int MaxRooms, System.Random rng, IProgress<int> progress, CancellationTokenSource token)
        {
            progress.Report(-1);
            // await Task.Delay(100);
            yield return null;
            int[] MapRoomID = new int[(int)RoomType.ROOM4 + 1];
            string[,] MapName = new string[MapTemp.GetLength(0), MapTemp.GetLength(1)];
            int i = 0;
            for (int y = 1; y < MapTemp.GetLength(1) - 1; y++)
            {
                for (int x = 1; x < MapTemp.GetLength(0) - 1; x++)
                {
                    i++;
                    progress.Report((int)((float)i / MapTemp.Length * 100f));
                    if (MapTemp[x, y] == 255)
                    {
                        string chkpt = "checkpoint1";
                        if (y+1 == Transitions[0])
                        {
                            chkpt = "checkpoint2";
                        }
                        Entity r = CreateRoom(GetZone(y), RoomType.ROOM2, x, 0, y, chkpt, rng, token);
                        r.Rotation = CreateFromYawPitchRoll(0f, 180f, 0f);
                        r.MarkTransformDirty(TransformDirtyFlags.Rotation);
                    }
                    else if (MapTemp[x, y] > 0)
                    {
                        int temp = Math.Min(MapTemp[x + 1, y], 1) + Math.Min(MapTemp[x - 1, y], 1) + Math.Min(MapTemp[x, y + 1], 1) + Math.Min(MapTemp[x, y - 1], 1);
                        switch (temp)
                        {
                            case 1:
                            {
                                RoomType type = RoomType.ROOM1;
                                if (MapRoomID[(int)type] < MaxRooms && string.IsNullOrEmpty(MapName[x, y]))
                                {
                                    if (!string.IsNullOrEmpty(MapRoom[(int)type, MapRoomID[(int)type]]))
                                    {
                                        MapName[x, y] = MapRoom[(int)type, MapRoomID[(int)type]];
                                    }
                                }

                                Entity r = CreateRoom(GetZone(y), type, x, 0, y, MapName[x, y], rng, token);
                                if (MapTemp[x, y+1] > 0)
                                {
                                    r.Rotation = CreateFromYawPitchRoll(0f, 180f, 0f);
                                }
                                else if (MapTemp[x-1, y] > 0)
                                {
                                    r.Rotation = CreateFromYawPitchRoll(0f, 90f, 0f);
                                }
                                else if (MapTemp[x+1, y] > 0)
                                {
                                    r.Rotation = CreateFromYawPitchRoll(0f, 270f, 0f);
                                }
                                else
                                {
                                    r.Rotation = CreateFromYawPitchRoll(0f, 0f, 0f);
                                }
                                r.MarkTransformDirty(TransformDirtyFlags.Rotation);
                                MapRoomID[(int)type]++;
                            }
                            break;
                            case 2:
                            {
                                if (MapTemp[x - 1, y] > 0 && MapTemp[x + 1, y] > 0)
                                {
                                    RoomType type = RoomType.ROOM2;
                                    if (MapRoomID[(int)type] < MaxRooms && string.IsNullOrEmpty(MapName[x, y]))
                                    {
                                        if (!string.IsNullOrEmpty(MapRoom[(int)type, MapRoomID[(int)type]]))
                                        {
                                            MapName[x, y] = MapRoom[(int)type, MapRoomID[(int)type]];
                                        }
                                    }
                                    Entity r = CreateRoom(GetZone(y), type, x, 0, y, MapName[x, y], rng, token);
                                    if (rng.Next(1, 3) == 1)
                                    {
                                        r.Rotation = CreateFromYawPitchRoll(0f, 270f, 0f);
                                    }
                                    else
                                    {
                                        r.Rotation = CreateFromYawPitchRoll(0f, 90f, 0f);
                                    }
                                    r.MarkTransformDirty(TransformDirtyFlags.Rotation);
                                    MapRoomID[(int)type]++;
                                }
                                else if (MapTemp[x, y - 1] > 0 && MapTemp[x, y + 1] > 0)
                                {
                                    RoomType type = RoomType.ROOM2;
                                    if (MapRoomID[(int)type] < MaxRooms && string.IsNullOrEmpty(MapName[x, y]))
                                    {
                                        if (!string.IsNullOrEmpty(MapRoom[(int)type, MapRoomID[(int)type]]))
                                        {
                                            MapName[x, y] = MapRoom[(int)type, MapRoomID[(int)type]];
                                        }
                                    }
                                    Entity r = CreateRoom(GetZone(y), type, x, 0, y, MapName[x, y], rng, token);
                                    if (rng.Next(1, 3) == 1)
                                    {
                                        r.Rotation = CreateFromYawPitchRoll(0f, 180f, 0f);
                                    }
                                    else
                                    {
                                        r.Rotation = CreateFromYawPitchRoll(0f, 0f, 0f);
                                    }
                                    r.MarkTransformDirty(TransformDirtyFlags.Rotation);
                                    MapRoomID[(int)type]++;
                                }
                                else
                                {
                                    RoomType type = RoomType.ROOM2C;
                                    if (MapRoomID[(int)type] < MaxRooms && string.IsNullOrEmpty(MapName[x, y]))
                                    {
                                        if (!string.IsNullOrEmpty(MapRoom[(int)type, MapRoomID[(int)type]]))
                                        {
                                            MapName[x, y] = MapRoom[(int)type, MapRoomID[(int)type]];
                                        }
                                    }
                                    Entity r = CreateRoom(GetZone(y), type, x, 0, y, MapName[x, y], rng, token);
                                    if (MapTemp[x - 1, y] > 0 && MapTemp[x, y + 1] > 0)
                                    {
                                        r.Rotation = CreateFromYawPitchRoll(0f, 90f, 0f);
                                    }
                                    else if (MapTemp[x + 1, y] > 0 && MapTemp[x, y + 1] > 0)
                                    {
                                        r.Rotation = CreateFromYawPitchRoll(0f, 180f, 0f);
                                    }
                                    else if (MapTemp[x - 1, y] > 0 && MapTemp[x, y - 1] > 0)
                                    {
                                        r.Rotation = CreateFromYawPitchRoll(0f, 0f, 0f);
                                    }
                                    else
                                    {
                                        r.Rotation = CreateFromYawPitchRoll(0f, 270f, 0f);
                                    }
                                    r.MarkTransformDirty(TransformDirtyFlags.Rotation);
                                    MapRoomID[(int)type]++;
                                }
                            }
                            break;
                            case 3:
                            {
                                RoomType type = RoomType.ROOM3;
                                if (MapRoomID[(int)type] < MaxRooms && string.IsNullOrEmpty(MapName[x, y]))
                                {
                                    if (!string.IsNullOrEmpty(MapRoom[(int)type, MapRoomID[(int)type]]))
                                    {
                                        MapName[x, y] = MapRoom[(int)type, MapRoomID[(int)type]];
                                    }
                                }
                                Entity r = CreateRoom(GetZone(y), type, x, 0, y, MapName[x, y], rng, token);
                                if (MapTemp[x, y - 1] <= 0)
                                {
                                    r.Rotation = CreateFromYawPitchRoll(0f, 180f, 0f);
                                }
                                else if (MapTemp[x - 1, y] <= 0)
                                {
                                    r.Rotation = CreateFromYawPitchRoll(0f, 270f, 0f);
                                }
                                else if (MapTemp[x + 1, y] <= 0)
                                {
                                    r.Rotation = CreateFromYawPitchRoll(0f, 90f, 0f);
                                }
                                else
                                {
                                    r.Rotation = CreateFromYawPitchRoll(0f, 0f, 0f);
                                }
                                r.MarkTransformDirty(TransformDirtyFlags.Rotation);
                                MapRoomID[(int)type]++;
                            }
                            break;
                            case 4:
                            {
                                RoomType type = RoomType.ROOM4;
                                if (MapRoomID[(int)type] < MaxRooms && string.IsNullOrEmpty(MapName[x, y]))
                                {
                                    if (!string.IsNullOrEmpty(MapRoom[(int)type, MapRoomID[(int)type]]))
                                    {
                                        MapName[x, y] = MapRoom[(int)type, MapRoomID[(int)type]];
                                    }
                                }
                                Entity r = CreateRoom(GetZone(y), type, x, 0, y, MapName[x, y], rng, token);
                                r.MarkTransformDirty(TransformDirtyFlags.Rotation);
                                MapRoomID[(int)type]++;
                            }
                            break;
                        }
                    }
                }
            }
            // await Task.Delay(100);
            yield return null;
            progress.Report(100);
        }

        public Entity CreateRoom(int zone, RoomType type, int x, int y, int z, string room_name, System.Random rng, CancellationTokenSource token)
        {
            RMeshEntity room = new RMeshEntity(room_name, data.GetRoomPath(room_name, type, zone, rng));
            UnspawnedEntities.Add(room);
            room.Position = new Vector3(x * 8f, y * 8f, z * 8f);
            room.MarkTransformDirty(TransformDirtyFlags.Position);
            return room;
        }

        public bool SetRoom(ref string[,] MapRoom, string room_name, RoomType type, int pos, int min_pos, int max_pos)
        {
            if (max_pos < min_pos)
            {
                Log.Warn(nameof(MapGenerator), $"Can't place {room_name}");
                return false;
            }
            
            bool looped = false;
            bool can_place = true;
            while (!string.IsNullOrEmpty(MapRoom[(int)type, pos]))
            {
                pos++;
                if (pos > max_pos)
                {
                    if (!looped)
                    {
                        pos = min_pos + 1;
                        looped = true;
                    }
                    else
                    {
                        can_place = false;
                        break;
                    }
                }
            }
            if (can_place)
            {
                MapRoom[(int)type, pos] = room_name;
                return true;
            }
            else
            {
                Log.Warn(nameof(MapGenerator), $"Can't place {room_name}");
                return false;
            }
        }
    }
}
