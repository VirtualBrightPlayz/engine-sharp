using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using Engine;
using Engine.Assets;
using Engine.Assets.Audio;
using Engine.Assets.Rendering;
using Engine.Game;
using Engine.Game.Entities;
using Engine.Game.Physics;
using ImGuiNET;
using Veldrid;

namespace Propnado
{
    public class NoClipPlayer : Entity
    {
        public InputHandler Input => MiscGlobals.GameInputHandler;
        public float speed = 2f;
        public float acceleration = 20f;
        public float deAcceleration = 1000000f;
        public Vector3 viewPos = Vector3.Zero;
        public Vector3 viewDirection = -Vector3.UnitZ;
        public Vector3 direction = -Vector3.UnitZ;
        public Vector2 lookAxis = Vector2.Zero;
        public Quaternion CameraRotation = Quaternion.Identity;
        public Vector3 desiredVelocity = Vector3.Zero;
        private bool wasEscPressed = false;
        public bool DebugMode = false;
        public Vector3 Velocity { get; set; } = Vector3.Zero;
        private int mouseSpeed = 10;
        private bool posDeltaFlipFlop = false;

        public NoClipPlayer() : base("NoClipPlayer")
        {
        }

        public override void PreDraw(Renderer renderer, double dt)
        {
            base.PreDraw(renderer, dt);
            UpdateLook();
            renderer.ViewMatrix = Matrix4x4.CreateLookAt(viewPos, viewPos + viewDirection, LocalUp);
            renderer.ViewPosition = viewPos;
            UpdateAudioPos();
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
            UpdateAudioPos();
            if (DebugMode)
            {
                if (ImGui.Begin("Test"))
                {
                    ImGui.Text("Test Window");
                    ImGui.Text($"Position {Position.ToString()}");
                    ImGui.Text($"Velocity {Velocity.ToString()}");
                    ImGui.InputFloat("Acceleration", ref acceleration);
                    ImGui.InputFloat("DeAcceleration", ref deAcceleration);
                    ImGui.InputFloat("Speed", ref speed);
                }
                ImGui.End();
            }
        }

        public void UpdateAudioPos()
        {
            AudioGlobals.Position = viewPos;
            float[] orientation = new float[6];
            orientation[0] = viewDirection.X;
            orientation[1] = viewDirection.Y;
            orientation[2] = viewDirection.Z;
            orientation[3] = LocalUp.X;
            orientation[4] = LocalUp.Y;
            orientation[5] = LocalUp.Z;
            AudioGlobals.OrientationRaw = orientation;
        }

        public override void Tick(double dt)
        {
            base.Tick(dt);
            UpdateLookRotation();
            UpdateMovement(dt);
            Position += Velocity * (float)dt;
            UpdateAudioPos();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void UpdateLook()
        {
            if (!MiscGlobals.IsFocused)
            {
                Input.IsMouseLocked = false;
            }
            if (Input.IsMouseLocked)
            {
                if (posDeltaFlipFlop)
                    Input.Position = new Vector2(RenderingGlobals.ViewSize.X / 2, RenderingGlobals.ViewSize.Y / 2);
                else
                {
                    lookAxis += Input.MouseDelta * Vector2.One * 0.25f;
                    lookAxis.Y = MathUtils.Clamp(lookAxis.Y, -89f, 89f);
                    UpdateLookRotation();
                }
                posDeltaFlipFlop = !posDeltaFlipFlop;
            }
            if (Input.IsKeyPressed(Veldrid.Key.Escape) && !wasEscPressed)
            {
                Input.IsMouseLocked = !Input.IsMouseLocked;
            }
            wasEscPressed = Input.IsKeyPressed(Veldrid.Key.Escape);
        }

        public void UpdateLookRotation()
        {
            Quaternion cameraRot = Quaternion.CreateFromYawPitchRoll(lookAxis.X * (MathF.PI / 180f), lookAxis.Y * (MathF.PI / 180f), 0f);
            CameraRotation = cameraRot;
            Rotation = Quaternion.CreateFromYawPitchRoll(lookAxis.X * (MathF.PI / 180f), 0f, 0f);
            QuaternionEx.Transform(-Vector3.UnitZ, cameraRot, out viewDirection);
            QuaternionEx.Transform(-Vector3.UnitZ, Rotation, out direction);
            MarkTransformDirty(TransformDirtyFlags.Rotation);
        }

        public void UpdateMovement(double dt)
        {
            if (Input.IsKeyPressed(Key.K))
                DebugMode = !Input.IsMouseLocked;

            Vector2 inputDirection = Vector2.Zero;
            if (Input.IsKeyPressed(Key.W))
                inputDirection.Y += 1f;
            if (Input.IsKeyPressed(Key.S))
                inputDirection.Y -= 1f;
            if (Input.IsKeyPressed(Key.A))
                inputDirection.X -= 1f;
            if (Input.IsKeyPressed(Key.D))
                inputDirection.X += 1f;
            float inputLength = inputDirection.LengthSquared();
            if (inputLength > 0)
                inputDirection /= MathF.Sqrt(inputLength);
            Vector2 targetVelocity = inputDirection * speed;
            Vector3 velocity = Velocity;
            float maxVelChange = (float)dt * acceleration;
            float maxVelChangeDec = (float)dt * deAcceleration;
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
                    velocity = MathUtils.MoveTowards(velocity, new Vector3(worldMovement.X, velocity.Y - 1f, worldMovement.Z), maxVelChange);
                }
                else
                {
                    var y = velocity.Y;
                    velocity = MathUtils.MoveTowards(velocity, new Vector3(0f, y, 0f), maxVelChangeDec);
                }
            }
            else
            {
                var y = velocity.Y;
                velocity = MathUtils.MoveTowards(velocity, new Vector3(0f, y, 0f), maxVelChangeDec);
            }
            float velLen = targetVelocity.Length();
            Velocity = velocity;
        }
    }
}