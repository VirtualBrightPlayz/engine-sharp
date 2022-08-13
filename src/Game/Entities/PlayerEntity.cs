using System;
using System.Diagnostics;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using Engine.Assets;
using Engine.Assets.Rendering;
using Engine.Game.Physics;
using Engine.VeldridSilk;
using ImGuiNET;
using Silk.NET.Input;

namespace Engine.Game.Entities
{
    public class PlayerEntity : PhysicsEntity
    {
        public IInputContext Input => Program.GameInputContext;
        public InputHandler InputHandler => Program.GameInputSnapshotHandler;
        public Capsule shape;
        public float speed = 5f;
        public float acceleration = 10f;
        public Vector3 viewDirection = -Vector3.UnitZ;
        public Vector3 direction = -Vector3.UnitZ;
        public Vector2 lookAxis = Vector2.Zero;
        public Quaternion CameraRotation = Quaternion.Identity;
        public Vector3 desiredVelocity = Vector3.Zero;
        private bool wasEscPressed = false;

        public PlayerEntity() : base("Player")
        {
            shape = new Capsule(0.6f, 1.8f);
            shapeIndex = Game.Simulation.Shapes.Add(shape);
            var inertia = shape.ComputeInertia(1f);
            bodyHandle = Game.Simulation.Bodies.Add(BodyDescription.CreateDynamic(Position, inertia, new CollidableDescription(shapeIndex.Value, 0.1f, float.MaxValue, ContinuousDetection.Passive), /*shape.Radius **/ 0.02f));
        }

        public override void PreDraw(double dt)
        {
            base.PreDraw(dt);
            UpdateLook();
            Renderer.Current.ViewMatrix = Matrix4x4.CreateLookAt(Position + Vector3.UnitY * shape.HalfLength, Position + Vector3.UnitY * shape.HalfLength + viewDirection, LocalUp);
        }

        public override void Draw(double dt)
        {
            base.Draw(dt);
            BodyReference body = Game.Simulation.Bodies[bodyHandle.Value];
            if (ImGui.Begin("Test"))
            {
                ImGui.Text("Test Window");
                ImGui.Text($"Position {Position.ToString()}");
                ImGui.Text($"Velocity {body.Velocity.Linear.ToString()}");
                ImGui.End();
            }
        }

        public override unsafe void Tick(double dt)
        {
            base.Tick(dt);
            UpdateMovement(dt);
            Program.GameAudio.SetListenerProperty(Silk.NET.OpenAL.ListenerVector3.Position, Position);
            float[] orientation = new float[6];
            orientation[0] = viewDirection.X;
            orientation[1] = viewDirection.Y;
            orientation[2] = viewDirection.Z;
            orientation[3] = LocalUp.X;
            orientation[4] = LocalUp.Y;
            orientation[5] = LocalUp.Z;
            fixed (float* d = &orientation[0])
            {
                Program.GameAudio.SetListenerProperty(Silk.NET.OpenAL.ListenerFloatArray.Orientation, d);
            }
        }

        public void UpdateLook()
        {
            if (!Program.IsFocused)
            {
                InputHandler.IsMouseLocked = false;
            }
            if (InputHandler.IsMouseLocked)
            {
                InputHandler.Position = new Vector2(Program.GameWindow.Size.X / 2, Program.GameWindow.Size.Y / 2);
                lookAxis += InputHandler.MouseDelta * Vector2.One * 0.25f;
                lookAxis.Y = MathUtils.Clamp(lookAxis.Y, -89f, 89f);
                Quaternion cameraRot = Quaternion.CreateFromYawPitchRoll(lookAxis.X * (MathF.PI / 180f), lookAxis.Y * (MathF.PI / 180f), 0f);
                CameraRotation = cameraRot;
                Rotation = Quaternion.CreateFromYawPitchRoll(lookAxis.X * (MathF.PI / 180f), 0f, 0f);
                QuaternionEx.Transform(-Vector3.UnitZ, cameraRot, out viewDirection);
                QuaternionEx.Transform(-Vector3.UnitZ, Rotation, out direction);
            }
            MarkTransformDirty(TransformDirtyFlags.Rotation);
            if (InputHandler.IsMouseDown(Veldrid.MouseButton.Right) && !wasEscPressed)
            {
                Program.GameAudio.GetListenerProperty(Silk.NET.OpenAL.ListenerVector3.Position, out Vector3 pos);
                InputHandler.IsMouseLocked = !InputHandler.IsMouseLocked;
            }
            wasEscPressed = InputHandler.IsMouseDown(Veldrid.MouseButton.Right);
        }

        public void UpdateMovement(double dt)
        {
            BodyReference body = Game.Simulation.Bodies[bodyHandle.Value];
            Vector2 inputDirection = Vector2.Zero;
            if (Input.Keyboards[0].IsKeyPressed(Key.W))
                inputDirection.Y += 1f;
            if (Input.Keyboards[0].IsKeyPressed(Key.S))
                inputDirection.Y -= 1f;
            if (Input.Keyboards[0].IsKeyPressed(Key.A))
                inputDirection.X -= 1f;
            if (Input.Keyboards[0].IsKeyPressed(Key.D))
                inputDirection.X += 1f;
            float inputLength = inputDirection.LengthSquared();
            if (inputLength > 0)
                inputDirection /= MathF.Sqrt(inputLength);
            Vector2 targetVelocity = inputDirection * speed;
            Vector3 velocity = body.Velocity.Linear;
            float maxVelChange = (float)dt * speed;
            if (!body.Awake && targetVelocity != Vector2.Zero)
            {
                Game.Simulation.Awakener.AwakenBody(bodyHandle.Value);
            }
            QuaternionEx.Transform(LocalUp, Rotation, out Vector3 charUp);
            Vector3 charRight = Vector3.Cross(direction, charUp);
            float rightLen = charRight.LengthSquared();
            if (rightLen > 0f)
            {
                charRight /= MathF.Sqrt(rightLen);
                Vector3 charForward = Vector3.Cross(charUp, charRight);
                Vector3 worldMovement = charRight * targetVelocity.X + charForward * targetVelocity.Y;
                if (worldMovement.LengthSquared() > 0f)
                {
                    velocity = MathUtils.MoveTowards(velocity, new Vector3(worldMovement.X, velocity.Y, worldMovement.Z), maxVelChange);
                }
                else
                {
                    velocity = MathUtils.MoveTowards(velocity, new Vector3(0f, velocity.Y, 0f), maxVelChange);
                }
            }
            else
            {
                velocity = MathUtils.MoveTowards(velocity, new Vector3(0f, velocity.Y, 0f), maxVelChange);
            }
            body.Velocity.Linear = velocity;
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}