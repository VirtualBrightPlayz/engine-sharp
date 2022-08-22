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

namespace GameBSrc
{
    public class BPlayerEntity : PhysicsEntity
    {
        public InputHandler Input => MiscGlobals.GameInputSnapshot;
        public Capsule shape;
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
        private float walkViewTimer;
        private float viewBobSpeed = 1f;
        private float viewBobAmount = 0.1f;
        private float upDownBob => MathF.Abs(walkViewTimer - MathF.Floor(walkViewTimer + 0.5f)) * viewBobAmount * 2f - viewBobAmount * 0.5f;
        private float leftRightBob => MathF.Abs(walkViewTimer * 0.5f - MathF.Floor(walkViewTimer * 0.5f + 0.5f)) * viewBobAmount * 2f - viewBobAmount * 0.5f;
        private float footstepInterval = 0.75f;
        private float prevFootstepTime;
        private float footstepTime;
        private AudioSource footstepSource;
        private Task<AudioClip>[] footstepClips = new Task<AudioClip>[]
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
            var inertia = shape.ComputeInertia(0.1f);
            bodyHandle = Game.Simulation.Bodies.Add(BodyDescription.CreateDynamic(Position, inertia, new CollidableDescription(shapeIndex.Value, 0.1f, float.MaxValue, ContinuousDetection.Continuous()), shape.Radius * 0.02f));
        }

        public override void PreDraw(Renderer renderer, double dt)
        {
            base.PreDraw(renderer, dt);
            UpdateLook();
            QuaternionEx.Transform(Vector3.UnitX, Rotation, out var localUnitX);
            viewPos = Position + Vector3.UnitY * shape.HalfLength + Vector3.UnitY * upDownBob + localUnitX * leftRightBob;
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
                if (BGame.Instance.enemy != null)
                {
                    ImGui.Text($"Can See Enemy {CanSee(BGame.Instance.enemy.Center)}");
                }
                ImGui.InputFloat("ViewBobSpeed", ref viewBobSpeed);
                ImGui.InputFloat("ViewBobAmount", ref viewBobAmount);
                ImGui.InputFloat("FootstepInterval", ref footstepInterval);
                ImGui.ColorEdit4("AmbientColor", ref ForwardConsts.AmbientColor, ImGuiColorEditFlags.Float);
                ImGui.ColorEdit4("LightColor", ref nextLightColor, ImGuiColorEditFlags.Float);
                ImGui.DragFloat4("FogData", ref BGame.Instance.fogData);
                ImGui.InputInt("MaxLights", ref ForwardConsts.MaxRealtimeLights);
                ImGui.InputFloat("Acceleration", ref acceleration);
                ImGui.InputFloat("DeAcceleration", ref deAcceleration);
                ImGui.InputFloat("Speed", ref speed);
                if (ImGui.Button("Kill"))
                {
                    BGame.Instance.killTimer = 1;
                }
                ImGui.End();
            }
        }

        public bool CanSee(Vector3 position)
        {
            Matrix4x4 viewProj = Matrix4x4.Multiply(Renderer.Current.ViewMatrix, Renderer.Current.ProjectionMatrix);
            var pos = Vector3.Transform(position, viewProj);
            return pos.X <= 1 && pos.X >= -1 && pos.Y <= 1 && pos.Y >= -1 && pos.Z > 0f;
        }

        public override unsafe void Tick(double dt)
        {
            base.Tick(dt);
            UpdateLookRotation();
            UpdateMovement(dt);
            if (MathF.Abs(Game.Simulation.Bodies[bodyHandle.Value].Velocity.Linear.Y) > 4f)
            {
                BGame.Instance.killTimer = Math.Max(BGame.Instance.killTimer, 1);
            }
            AudioGlobals.GameAudio.SetListenerProperty(Silk.NET.OpenAL.ListenerVector3.Position, Position);
            float[] orientation = new float[6];
            orientation[0] = viewDirection.X;
            orientation[1] = viewDirection.Y;
            orientation[2] = viewDirection.Z;
            orientation[3] = LocalUp.X;
            orientation[4] = LocalUp.Y;
            orientation[5] = LocalUp.Z;
            fixed (float* d = &orientation[0])
            {
                AudioGlobals.GameAudio.SetListenerProperty(Silk.NET.OpenAL.ListenerFloatArray.Orientation, d);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            footstepSource.Dispose();
        }

        public void UpdateLook()
        {
            if (!MiscGlobals.IsFocused)
            {
                Input.IsMouseLocked = false;
            }
            if (Input.IsMouseLocked)
            {
                Input.Position = new Vector2(RenderingGlobals.ViewSize.X / 2, RenderingGlobals.ViewSize.Y / 2);
                lookAxis += Input.MouseDelta * Vector2.One * 0.25f;
                lookAxis.Y = MathUtils.Clamp(lookAxis.Y, -89f, 89f);
                UpdateLookRotation();
            }
            if (Input.IsMouseDown(Veldrid.MouseButton.Right) && !wasEscPressed)
            {
                BGame.Instance.DebugMode = Input.IsMouseLocked;
                Input.IsMouseLocked = !Input.IsMouseLocked;
            }
            wasEscPressed = Input.IsMouseDown(Veldrid.MouseButton.Right);
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

        public async void UpdateMovement(double dt)
        {
            BodyReference body = Game.Simulation.Bodies[bodyHandle.Value];
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
            Vector3 velocity = body.Velocity.Linear;
            float maxVelChange = (float)dt * acceleration;
            float maxVelChangeDec = (float)dt * deAcceleration;
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
            if (prevFootstepTime + footstepInterval < footstepTime)
            {
                footstepSource.SetBuffer(await footstepClips[Random.Shared.Next(footstepClips.Length)]);
                footstepSource.Play();
                prevFootstepTime = footstepTime;
            }
            walkViewTimer += (velLen > 0f ? 1f : 0f) * viewBobSpeed * (float)dt;
            footstepTime += (float)dt * (velLen > 0f ? 1f : 0f);
            body.Velocity.Linear = velocity;
        }
    }
}