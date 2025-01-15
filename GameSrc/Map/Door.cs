using System;
using System.IO;
using System.Numerics;
using Engine.Assets.Rendering;
using Engine.Game;
using Engine.Game.Entities;
using GameSrc;

namespace GameSrc.Map
{
    public class Door : Entity, IInteractable
    {
        public StaticModelEntity frame;
        public StaticModelEntity obj1;
        public StaticModelEntity obj2;
        public StaticModelEntity[] buttons = new StaticModelEntity[2];
        public float openState = 0;
        public bool IsOpen = false;

        public static string DoorFrame => Path.Combine(SCPCB.Instance.Data.MapDir, "DoorFrame.x");
        public static string DoorObj => Path.Combine(SCPCB.Instance.Data.MapDir, "Door01.x");
        public static string ButtonObj => Path.Combine(SCPCB.Instance.Data.MapDir, "Button.x");

        public Door(string name) : base(name)
        {
            Material dummy = new Material(name, SCPCB.shader);
            frame = new StaticModelEntity(name, DoorFrame, dummy);
            frame.Create(true);
            obj1 = new StaticModelEntity(name, DoorObj, dummy);
            obj1.Create(true);
            obj2 = new StaticModelEntity(name, DoorObj, dummy);
            obj2.Create(true);
            buttons[0] = new StaticModelEntity(name, ButtonObj, dummy);
            buttons[0].Create(true);
            InteractHandler.Interactables.TryAdd(buttons[0].staticHandle.Value, this);
            buttons[1] = new StaticModelEntity(name, ButtonObj, dummy);
            buttons[1].Create(true);
            InteractHandler.Interactables.TryAdd(buttons[1].staticHandle.Value, this);
            MarkTransformDirty(TransformDirtyFlags.Position | TransformDirtyFlags.Rotation | TransformDirtyFlags.Scale);
        }

        public override void MarkTransformDirty(TransformDirtyFlags flags)
        {
            base.MarkTransformDirty(flags);
            if (frame != null)
            {
                frame.Position = Position;
                frame.Rotation = Rotation;
                frame.Scale = Scale * (Vector3.One * 8f / 2048f);
                frame.MarkTransformDirty(flags);
            }
            if (obj1 != null)
            {
                obj1.Position = Position + new Vector3(MathF.Sin(openState) / 80f, 0f, 0f);
                obj1.Rotation = Rotation;
                obj1.Scale = Scale * new Vector3(204f, 312f, 16f) * RMeshModel.RoomScale / obj1.Model.MeshSize;
                obj1.MarkTransformDirty(flags);
            }
            if (obj2 != null)
            {
                obj2.Position = Position;
                obj2.Rotation = Rotation * Quaternion.CreateFromAxisAngle(Vector3.UnitY, 180f * (MathF.PI / 180f));
                obj2.Scale = Scale * new Vector3(204f, 312f, 16f) * RMeshModel.RoomScale / obj2.Model.MeshSize;
                obj2.MarkTransformDirty(flags);
            }
            {
                if (buttons[0] != null)
                {
                    // buttons[0].Position = Position + Scale * new Vector3(432f * RMeshModel.RoomScaleFloat, 0.7f, -192f * RMeshModel.RoomScaleFloat);
                    buttons[0].Position = Position + Scale * new Vector3(0.6f, 0.7f, 0.1f);
                    buttons[0].Rotation = Rotation;
                    buttons[0].Scale = Scale * Vector3.One * 0.03f;
                    buttons[0].MarkTransformDirty(flags);
                }
                if (buttons[1] != null)
                {
                    buttons[1].Position = Position + Scale * new Vector3(-0.6f, 0.7f, -0.1f);
                    buttons[1].Rotation = Rotation * Quaternion.CreateFromAxisAngle(Vector3.UnitY, 180f * (MathF.PI / 180f));
                    buttons[1].Scale = Scale * Vector3.One * 0.03f;
                    buttons[1].MarkTransformDirty(flags);
                }
            }
        }

        public override void PreDraw(Renderer renderer, double dt)
        {
            base.PreDraw(renderer, dt);
            frame?.PreDraw(renderer, dt);
            obj1?.PreDraw(renderer, dt);
            obj2?.PreDraw(renderer, dt);
            buttons[0]?.PreDraw(renderer, dt);
            buttons[1]?.PreDraw(renderer, dt);
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
            frame?.Draw(renderer, dt);
            obj1?.Draw(renderer, dt);
            obj2?.Draw(renderer, dt);
            buttons[0]?.Draw(renderer, dt);
            buttons[1]?.Draw(renderer, dt);
        }

        public override void Tick(double dt)
        {
            base.Tick(dt);
            frame?.Tick(dt);
            obj1?.Tick(dt);
            obj2?.Tick(dt);
            buttons[0]?.Tick(dt);
            buttons[1]?.Tick(dt);
            if (IsOpen)
            {
                openState = MathUtils.Clamp(openState + (float)dt, 0f, 180f);
                if (openState < 180f)
                {
                    MarkTransformDirty(TransformDirtyFlags.Position);
                }
            }
            else
            {
                openState = MathUtils.Clamp(openState - (float)dt, 0f, 180f);
                if (openState > 0f)
                {
                    MarkTransformDirty(TransformDirtyFlags.Position);
                }
            }
        }

        public override void Dispose()
        {
            frame?.Dispose();
            frame = null;
            obj1?.Dispose();
            obj1 = null;
            obj2?.Dispose();
            obj2 = null;
            if (buttons[0] != null && buttons[0].staticHandle.HasValue)
                InteractHandler.Interactables.TryRemove(buttons[0].staticHandle.Value, out _);
            buttons[0]?.Dispose();
            buttons[0] = null;
            if (buttons[1] != null && buttons[1].staticHandle.HasValue)
            InteractHandler.Interactables.TryRemove(buttons[1].staticHandle.Value, out _);
            buttons[1]?.Dispose();
            buttons[1] = null;
            base.Dispose();
        }

        public void Press()
        {
            Log.Info(nameof(Door), "Pressed!");
            IsOpen = !IsOpen;
        }
    }
}