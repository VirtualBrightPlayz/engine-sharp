using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using BepuPhysics;
using BepuUtilities;
using BepuUtilities.Memory;
using Engine;
using Engine.Assets;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using Engine.Game;
using Engine.Game.Entities;
using GameSrc.Map;
using GameSrc.NPCs;
using ImGuiNET;
using VirtualBright.Util;

namespace GameSrc
{
    public class SCPCB : GameApp
    {
        public static SCPCB Instance { get; private set; }
        public override string Name => "SCP - Containment Breach";
        public GameData Data { get; private set; }
        public MapGenerator MapGen { get; private set; }
        public SCPCBPlayerEntity player;
        public MenuEntity Menu { get; private set; }
        public MusicHandler Music { get; private set; }
        public AStar NavMap { get; private set; }
        public SCP173 npc;
        public bool ShouldReturnToMenu = false;
        public static GraphicsShader shader { get; set; }

        public static ConcurrentDictionary<string, RMeshModel> RMeshModels { get; private set; } = new ConcurrentDictionary<string, RMeshModel>();
        public static ConcurrentDictionary<string, Texture2D> Textures { get; private set; } = new ConcurrentDictionary<string, Texture2D>();
        public static ConcurrentDictionary<string, Model> Models { get; private set; } = new ConcurrentDictionary<string, Model>();

        public static Texture2D GetTexture(string path)
        {
            if (Textures.TryGetValue(path, out var tex))
                return tex;
            var tex2 = new Texture2D(path);
            Textures.TryAdd(path, tex2);
            return tex2;
        }

        public SCPCB() : base()
        {
            Instance = this;
            Data = new GameData("scpcb");
            MapGen = new MapGenerator()
            {
                data = Data,
                RandomSeed = Random.Shared.Next(),
            };
        }

        public override void Setup()
        {
            base.Setup();
            RenderingGlobals.Window.Title = Name;
            RenderingGlobals.Window.WindowState = Veldrid.WindowState.BorderlessFullScreen;
            UIExt.LoadFonts();
            Menu = new MenuEntity();
            Entities.Add(Menu);
            Music = new MusicHandler();
            Entities.Add(Music);
            NavMap = new AStar(Simulation);
            shader ??= new GraphicsShader("Shaders/MainMesh");
        }

        public void SpawnPlayer()
        {
            if (player != null)
            {
                Entities.Remove(player);
                player.Dispose();
                player = null;
            }
            player = new SCPCBPlayerEntity();
            player.Position = ((RMeshEntity)Entities.First(x => x is RMeshEntity)).PlayerStart;
            player.MarkTransformDirty(TransformDirtyFlags.Position);
            Entities.Add(player);
            Music.Music = MusicHandler.MusicType.LightContainment;
            RebuildNavMap();
            if (npc != null)
            {
                Entities.Remove(npc);
                npc.Dispose();
                npc = null;
            }
            npc = new SCP173("NPC", Path.Combine(Data.GFXDir, "npcs", "173_2.b3d"), new Material("temp", shader));
            npc.Position = player.Position;
            npc.MarkTransformDirty(TransformDirtyFlags.Position);
            Entities.Add(npc);
        }

        public void QuitToMenu()
        {
            ShouldReturnToMenu = false;
            Entities.Remove(npc);
            npc.Dispose();
            npc = null;
            MapGen.UnspawnedEntities.Clear();
            foreach (var ent in Entities.Where(x => x is RMeshEntity || x is Door).ToArray())
            {
                Entities.Remove(ent);
                ent.Dispose();
            }
            if (player != null)
            {
                Entities.Remove(player);
                player.Dispose();
                player = null;
            }
            Menu.SetMenuState(MenuEntity.MenuState.MainMenu);
            Music.Music = MusicHandler.MusicType.None;
        }

        public void RebuildNavMap()
        {
            NavMap.Map.Clear();
            foreach (RMeshEntity rmesh in Entities.Where(x => x is RMeshEntity))
            {
                NavMap.Map.AddRange(rmesh.NavPoints.Select(x => new AStarNode(x)));
            }
            NavMap.RebuildMap();
        }

        public override void Draw(Renderer renderer, double dt)
        {
            ForwardConsts.UpdateUniforms(renderer);
            UIExt.BeginDraw();
            DebugGlobals.DrawDebugWindow();
            base.Draw(renderer, dt);
        }

        public override void Tick(double dt)
        {
            base.Tick(dt);
            TimeScale = MiscGlobals.IsFocused ? 1f : 0f;
            if (ShouldReturnToMenu)
                QuitToMenu();
        }

        public override void Dispose()
        {
            foreach (var ent in Entities)
            {
                ent.Dispose();
            }
            Entities.Clear();
            base.Dispose();
        }
    }
}