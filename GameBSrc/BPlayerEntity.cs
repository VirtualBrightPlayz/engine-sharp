using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
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
using Engine.VeldridSilk;
using ImGuiNET;
using Silk.NET.Input;

namespace GameBSrc
{
    public class BPlayerEntity : PhysicsEntity
    {
        public IInputContext Input => Program.GameInputContext;
        public InputHandler InputHandler => Program.GameInputSnapshotHandler;
        public Capsule shape;
        public float speed = 2f;
        public Vector3 viewPos = Vector3.Zero;
        public Vector3 viewDirection = -Vector3.UnitZ;
        public Vector3 direction = -Vector3.UnitZ;
        public Vector2 lookAxis = Vector2.Zero;
        public Quaternion CameraRotation = Quaternion.Identity;
        public Vector3 desiredVelocity = Vector3.Zero;
        private bool wasEscPressed = false;
        private float walkViewTimer;
        private float viewBobSpeed = 7.5f;
        private float viewBobAmount = 0.025f;
        private float upDownBob => MathF.Sin(walkViewTimer) * viewBobAmount;
        private float leftRightBob => MathF.Sin(walkViewTimer * 0.5f) * viewBobAmount;
        private float footstepInterval = 0.75f;
        private float prevFootstepTime;
        private float footstepTime;
        private AudioSource footstepSource;
        private AudioClip[] footstepClips = new AudioClip[]
        {
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "step.wav")),
        };
        private Vector4 nextLightColor = Vector4.One;

        public BPlayerEntity() : base("Player")
        {
            footstepSource = new AudioSource("PlayerFootsteps");
            footstepSource.MinGain = 1f;
            footstepSource.MaxGain = 1f;
            shape = new Capsule(0.25f, 1.25f);
            shapeIndex = Game.Simulation.Shapes.Add(shape);
            var inertia = shape.ComputeInertia(1f);
            bodyHandle = Game.Simulation.Bodies.Add(BodyDescription.CreateDynamic(Position, inertia, new CollidableDescription(shapeIndex.Value, 0.1f, float.MaxValue, ContinuousDetection.Discrete), shape.Radius * 0.02f));
        }

        public override void PreDraw(Renderer renderer, double dt)
        {
            base.PreDraw(renderer, dt);
            UpdateLook();
            viewPos = Position + Vector3.UnitY * shape.HalfLength + Vector3.UnitY * upDownBob;
            QuaternionEx.Transform(LocalUp, Quaternion.CreateFromAxisAngle(viewDirection, leftRightBob * 5f * (MathF.PI / 180f)), out Vector3 up);
            renderer.ViewMatrix = Matrix4x4.CreateLookAt(viewPos, viewPos + viewDirection, up);
            footstepSource.Position = viewPos;
            renderer.ViewPosition = viewPos;
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
            BodyReference body = Game.Simulation.Bodies[bodyHandle.Value];
            if (BGame.Instance.DebugMode && ImGui.Begin("Test"))
            {
                ImGui.Text("Test Window");
                ImGui.Text($"Position {Position.ToString()}");
                ImGui.Text($"Velocity {body.Velocity.Linear.ToString()}");
                ImGui.Text($"Current Floor: {BGame.Instance.currentFloor} Action {BGame.Instance.floorActions[BGame.Instance.currentFloor]} Timer {BGame.Instance.floorTimers[BGame.Instance.currentFloor]}");
                ImGui.Text($"Next Floor: {BGame.Instance.currentFloor+1} Action {BGame.Instance.floorActions[BGame.Instance.currentFloor+1]} Timer {BGame.Instance.floorTimers[BGame.Instance.currentFloor+1]}");
                ImGui.InputFloat("ViewBobSpeed", ref viewBobSpeed);
                ImGui.InputFloat("ViewBobAmount", ref viewBobAmount);
                ImGui.InputFloat("FootstepInterval", ref footstepInterval);
                ImGui.ColorEdit4("AmbientColor", ref ForwardConsts.AmbientColor, ImGuiColorEditFlags.Float);
                ImGui.ColorEdit4("LightColor", ref nextLightColor, ImGuiColorEditFlags.Float);
                ImGui.DragFloat4("FogData", ref BGame.Instance.fogData);
                ImGui.InputInt("MaxLights", ref ForwardConsts.MaxRealtimeLights);
                if (ImGui.Button("Kill"))
                {
                    BGame.Instance.killTimer = 1;
                }
                ImGui.End();
            }
        }

        public override unsafe void Tick(double dt)
        {
            base.Tick(dt);
            UpdateLookRotation();
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

        public override void Dispose()
        {
            base.Dispose();
            footstepSource.Dispose();
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
                UpdateLookRotation();
            }
            MarkTransformDirty(TransformDirtyFlags.Rotation);
            if (InputHandler.IsMouseDown(Veldrid.MouseButton.Right) && !wasEscPressed)
            {
                BGame.Instance.DebugMode = InputHandler.IsMouseLocked;
                InputHandler.IsMouseLocked = !InputHandler.IsMouseLocked;
            }
            wasEscPressed = InputHandler.IsMouseDown(Veldrid.MouseButton.Right);
        }

        public void UpdateLookRotation()
        {
            Quaternion cameraRot = Quaternion.CreateFromYawPitchRoll(lookAxis.X * (MathF.PI / 180f), lookAxis.Y * (MathF.PI / 180f), 0f);
            CameraRotation = cameraRot;
            Rotation = Quaternion.CreateFromYawPitchRoll(lookAxis.X * (MathF.PI / 180f), 0f, 0f);
            QuaternionEx.Transform(-Vector3.UnitZ, cameraRot, out viewDirection);
            QuaternionEx.Transform(-Vector3.UnitZ, Rotation, out direction);
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
                    var y = velocity.Y;
                    velocity = MathUtils.MoveTowards(velocity, new Vector3(0f, 0f, 0f), maxVelChange);
                }
            }
            else
            {
                var y = velocity.Y;
                velocity = MathUtils.MoveTowards(velocity, new Vector3(0f, 0f, 0f), maxVelChange);
            }
            float velLen = targetVelocity.Length();
            if (prevFootstepTime + footstepInterval < footstepTime)
            {
                footstepSource.SetBuffer(footstepClips[Random.Shared.Next(footstepClips.Length)]);
                footstepSource.Play();
                prevFootstepTime = footstepTime;
            }
            walkViewTimer += (velLen > 0f ? 1f : 0f) * viewBobSpeed * (float)dt;
            footstepTime += (float)dt * (velLen > 0f ? 1f : 0f);
            body.Velocity.Linear = velocity;
        }
    }
}