using System;
using System.Linq;
using System.Numerics;
using BepuPhysics;
using BepuUtilities;
using BepuUtilities.Memory;
using Engine;
using Engine.Game;
using Engine.Game.Entities;
using ImGuiNET;

namespace GameSrc
{
    public class SCPCB : GameApp
    {
        public static SCPCB Instance => Program.Game as SCPCB;
        public override string Name => "SCP - CSharp";
        public GameData Data { get; private set; }
        public MapGenerator MapGen { get; private set; }
        public SCPCBPlayerEntity player;
        private string rmeshPath = string.Empty;
        public MenuEntity Menu { get; private set; }

        public SCPCB() : base()
        {
            Data = new GameData("Game");
            MapGen = new MapGenerator()
            {
                data = Data,
            };
        }

        public override void Setup()
        {
            base.Setup();
            /*
            Entities.Add(new RMeshEntity("h", "SCPCB/GFX/map/173bright_opt.rmesh"));
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

        public override void Draw(double dt)
        {
            UIExt.BeginDraw();
            base.Draw(dt);
        }

        public override void Tick(double dt)
        {
            base.Tick(dt);
            TimeScale = Program.IsFocused ? 1f : 0f;
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