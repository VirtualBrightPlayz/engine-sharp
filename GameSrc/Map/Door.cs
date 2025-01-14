using System;
using System.IO;
using System.Numerics;
using Engine.Assets.Rendering;
using Engine.Game.Entities;
using GameSrc;

public class Door : Entity
{
    public StaticModelEntity frame;
    public StaticModelEntity obj1;
    public StaticModelEntity obj2;

    public static string DoorFrame => Path.Combine(SCPCB.Instance.Data.MapDir, "DoorFrame.x");
    public static string DoorObj => Path.Combine(SCPCB.Instance.Data.MapDir, "Door01.x");

    public Door(string name) : base(name)
    {
        frame = new StaticModelEntity(name, DoorFrame, new Material(name, SCPCB.shader));
        frame.Create(true);
        obj1 = new StaticModelEntity(name, DoorObj, new Material(name, SCPCB.shader));
        obj1.Create(true);
        obj2 = new StaticModelEntity(name, DoorObj, new Material(name, SCPCB.shader));
        obj2.Create(true);
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
            obj1.Position = Position;
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
    }

    public override void PreDraw(Renderer renderer, double dt)
    {
        base.PreDraw(renderer, dt);
        frame?.PreDraw(renderer, dt);
        obj1?.PreDraw(renderer, dt);
        obj2?.PreDraw(renderer, dt);
    }

    public override void Draw(Renderer renderer, double dt)
    {
        base.Draw(renderer, dt);
        frame?.Draw(renderer, dt);
        obj1?.Draw(renderer, dt);
        obj2?.Draw(renderer, dt);
    }

    public override void Tick(double dt)
    {
        base.Tick(dt);
        frame?.Tick(dt);
        obj1?.Tick(dt);
        obj2?.Tick(dt);
    }

    public override void Dispose()
    {
        frame?.Dispose();
        frame = null;
        obj1?.Dispose();
        obj1 = null;
        obj2?.Dispose();
        obj2 = null;
        base.Dispose();
    }
}