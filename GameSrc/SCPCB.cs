using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BepuPhysics;
using BepuUtilities;
using BepuUtilities.Memory;
using Engine;
using Engine.Assets;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using Engine.Game;
using Engine.Game.Entities;
using ImGuiNET;

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

        public static ConcurrentDictionary<string, RMeshModel> RMeshModels { get; private set; } = new ConcurrentDictionary<string, RMeshModel>();
        public static ConcurrentDictionary<string, Texture2D> Textures { get; private set; } = new ConcurrentDictionary<string, Texture2D>();

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
            UIExt.LoadFonts();
            RenderingGlobals.Window.Title = Name;
            RenderingGlobals.Window.WindowState = Veldrid.WindowState.BorderlessFullScreen;
            Menu = new MenuEntity();
            Entities.Add(Menu);
            Music = new MusicHandler();
            Entities.Add(Music);
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
            player.Position = ((RMeshEntity)Entities.First(x => x is RMeshEntity)).PlayerStart + Vector3.UnitY * 1f;
            player.MarkTransformDirty(TransformDirtyFlags.Position);
            Entities.Add(player);
            Music.Music = MusicHandler.MusicType.LightContainment;
        }

        public override void Draw(Renderer renderer, double dt)
        {
            ForwardConsts.UpdateUniforms(renderer);
            UIExt.BeginDraw();
            base.Draw(renderer, dt);
        }

        public override void Tick(double dt)
        {
            base.Tick(dt);
            TimeScale = MiscGlobals.IsFocused ? 1f : 0f;
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