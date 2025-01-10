using System;
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
        public override string Name => "SCP - CSharp";
        public GameData Data { get; private set; }
        public MapGenerator MapGen { get; private set; }
        public SCPCBPlayerEntity player;
        private string rmeshPath = string.Empty;
        public MenuEntity Menu { get; private set; }

        public static Dictionary<string, RMeshModel> RMeshModels { get; private set; } = new Dictionary<string, RMeshModel>();
        public static Dictionary<string, Texture2D> Textures { get; private set; } = new Dictionary<string, Texture2D>();

        public static Texture2D GetTexture(string path)
        {
            if (Textures.TryGetValue(path, out var tex))
                return tex;
            var tex2 = new Texture2D(path);
            Textures.Add(path, tex2);
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
            /*
            Entities.Add(new RMeshEntity("h", "Game/GFX/map/173_opt.rmesh"));
            SpawnPlayer();
            return;
            */
            Menu = new MenuEntity();
            Entities.Add(Menu);
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
            player.Position = Entities.First(x => x is RMeshEntity).Position + Vector3.UnitY * 3f;
            player.MarkTransformDirty(TransformDirtyFlags.Position);
            Entities.Add(player);
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