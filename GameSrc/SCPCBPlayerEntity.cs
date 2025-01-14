using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.Trees;
using BepuUtilities;
using Engine;
using Engine.Assets;
using Engine.Assets.Audio;
using Engine.Assets.Rendering;
using Engine.Game;
using Engine.Game.Entities;
using Engine.Game.Physics;
using ImGuiNET;

namespace GameSrc
{
    public class SCPCBPlayerEntity : PhysicsEntity, IRayHitHandler
    {
        public InputHandler InputHandler => MiscGlobals.GameInputHandler;
        public Capsule shape;
        public float speed = 2f;
        public float acceleration = 100f;
        public Vector3 viewDirection = -Vector3.UnitZ;
        public Vector3 viewDirectionUp = Vector3.UnitY;
        public Vector3 direction = -Vector3.UnitZ;
        public Vector2 lookAxis = Vector2.Zero;
        public Quaternion CameraRotation = Quaternion.Identity;
        public Vector3 desiredVelocity = Vector3.Zero;
        private bool wasEscPressed = false;
        private float walkViewTimer;
        private float viewBobSpeed = 7.5f;
        private float viewBobAmount = 0.01f;
        private float upDownBob => MathF.Sin(walkViewTimer) * viewBobAmount;
        private float leftRightBob => MathF.Cos(walkViewTimer) * viewBobAmount;
        private float footstepInterval = 0.75f;
        private float prevFootstepTime;
        private float footstepTime;
        private AudioSource footstepSource;
        private AudioClip[] footstepClips = new AudioClip[]
        {
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "Step1.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "Step2.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "Step3.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "Step4.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "Step5.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "Step6.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "Step7.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "Step8.ogg")),
        };
        private AudioClip[] footstepMetalClips = new AudioClip[]
        {
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "StepMetal1.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "StepMetal2.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "StepMetal3.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "StepMetal4.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "StepMetal5.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "StepMetal6.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "StepMetal7.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Step", "StepMetal8.ogg")),
        };
        public static float MaxRoomRenderDistance = 24f;

        public SCPCBPlayerEntity() : base("Player")
        {
            footstepSource = new AudioSource("PlayerFootsteps");
            footstepSource.MinGain = 1f;
            footstepSource.MaxGain = 1f;
            shape = new Capsule(0.2f, 0.5f);
            shapeIndex = Game.Simulation.Shapes.Add(shape);
            var inertia = shape.ComputeInertia(1f);
            bodyHandle = Game.Simulation.Bodies.Add(BodyDescription.CreateDynamic(Position, inertia, new CollidableDescription(shapeIndex.Value, 0.1f, float.PositiveInfinity, ContinuousDetection.Continuous()), shape.Radius * 0.02f));
        }

        public bool AllowTest(CollidableReference collidable)
        {
            return collidable.Mobility == CollidableMobility.Static && RMeshEntity.FloorLookup.ContainsKey(collidable.StaticHandle);
        }

        public bool AllowTest(CollidableReference collidable, int childIndex)
        {
            return collidable.Mobility == CollidableMobility.Static && RMeshEntity.FloorLookup.ContainsKey(collidable.StaticHandle);
        }

        public void OnRayHit(in RayData ray, ref float maximumT, float t, in Vector3 normal, CollidableReference collidable, int childIndex)
        {
            switch (ray.Id)
            {
                case 0: // footsteps
                {
                    if (RMeshEntity.FloorLookup.TryGetValue(collidable.StaticHandle, out string floorTexture))
                    {
                        if (floorTexture == "1")
                        {
                            footstepSource.SetBuffer(footstepMetalClips[Random.Shared.Next(footstepMetalClips.Length)]);
                        }
                        else
                        {
                            footstepSource.SetBuffer(footstepClips[Random.Shared.Next(footstepClips.Length)]);
                        }
                    }
                    else
                    {
                        footstepSource.SetBuffer(footstepClips[Random.Shared.Next(footstepClips.Length)]);
                    }
                    footstepSource.Play();
                    break;
                }
            }
        }

        public override void PreDraw(Renderer renderer, double dt)
        {
            base.PreDraw(renderer, dt);
            UpdateLook(dt);
            Vector3 viewPos = Position + Vector3.UnitY * (shape.HalfLength + shape.Radius) + Vector3.UnitY * upDownBob;
            QuaternionEx.Transform(LocalUp, Quaternion.CreateFromAxisAngle(viewDirection, leftRightBob * (MathF.PI / 180f)), out Vector3 up);
            renderer.ViewMatrix = Matrix4x4.CreateLookAt(viewPos, viewPos + viewDirection, up);
            footstepSource.Position = viewPos;
            renderer.ViewPosition = viewPos;
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
            BodyReference body = Game.Simulation.Bodies[bodyHandle.Value];
            if (ImGui.Begin("Debug Window"))
            {
                ImGui.Text($"FPS: {MiscGlobals.FPS}");
                ImGui.Text($"Position {Position}");
                ImGui.Text($"Velocity {body.Velocity.Linear}");
                ImGui.InputFloat("ViewBobSpeed", ref viewBobSpeed);
                ImGui.InputFloat("ViewBobAmount", ref viewBobAmount);
                ImGui.InputFloat("FootstepInterval", ref footstepInterval);
                ImGui.ColorEdit4("AmbientColor", ref ForwardConsts.AmbientColor, ImGuiColorEditFlags.Float);
                ImGui.InputFloat("MaxRoomRenderDistance", ref MaxRoomRenderDistance);
                ImGui.InputInt("MaxLights", ref ForwardConsts.MaxRealtimeLights);
                ImGui.SliderFloat("Speed", ref speed, 1f, 10f);
                if (ImGui.Button("Quit"))
                {
                    SCPCB.Instance.ShouldReturnToMenu = true;
                }
            }
            ImGui.End();
        }

        public override unsafe void Tick(double dt)
        {
            base.Tick(dt);
            // UpdateLook(dt);
            UpdateMovement(dt);
            AudioGlobals.Position = Position;
            float[] orientation = new float[6];
            orientation[0] = viewDirection.X;
            orientation[1] = viewDirection.Y;
            orientation[2] = viewDirection.Z;
            orientation[3] = viewDirectionUp.X;
            orientation[4] = viewDirectionUp.Y;
            orientation[5] = viewDirectionUp.Z;
            AudioGlobals.OrientationRaw = orientation;
        }

        public override void Dispose()
        {
            base.Dispose();
            footstepSource.Dispose();
        }

        public void UpdateLook(double delta)
        {
            if (!MiscGlobals.IsFocused)
            {
                InputHandler.IsMouseLocked = false;
            }
            if (InputHandler.IsMouseLocked)
            {
                {
                    InputHandler.Position = new Vector2(RenderingGlobals.Window.Width / 2, RenderingGlobals.Window.Height / 2);
                    lookAxis += InputHandler.MouseDelta * Vector2.One * 0.1f;
                    lookAxis.Y = MathUtils.Clamp(lookAxis.Y, -89f, 89f);
                }
                Quaternion cameraRot = Quaternion.CreateFromYawPitchRoll(lookAxis.X * (MathF.PI / 180f), lookAxis.Y * (MathF.PI / 180f), 0f);
                CameraRotation = cameraRot;
                Rotation = Quaternion.CreateFromYawPitchRoll(lookAxis.X * (MathF.PI / 180f), 0f, 0f);
                QuaternionEx.Transform(-Vector3.UnitZ, cameraRot, out viewDirection);
                QuaternionEx.Transform(Vector3.UnitY, cameraRot, out viewDirectionUp);
                QuaternionEx.Transform(-Vector3.UnitZ, Rotation, out direction);
            }
            MarkTransformDirty(TransformDirtyFlags.Rotation);
            if (InputHandler.IsKeyPressed(Veldrid.Key.Escape) && !wasEscPressed)
            {
                InputHandler.IsMouseLocked = !InputHandler.IsMouseLocked;
            }
            wasEscPressed = InputHandler.IsKeyPressed(Veldrid.Key.Escape);
        }

        public void UpdateMovement(double dt)
        {
            BodyReference body = Game.Simulation.Bodies[bodyHandle.Value];
            Vector2 inputDirection = Vector2.Zero;
            if (InputHandler.IsKeyPressed(Veldrid.Key.W))
                inputDirection.Y += 1f;
            if (InputHandler.IsKeyPressed(Veldrid.Key.S))
                inputDirection.Y -= 1f;
            if (InputHandler.IsKeyPressed(Veldrid.Key.A))
                inputDirection.X -= 1f;
            if (InputHandler.IsKeyPressed(Veldrid.Key.D))
                inputDirection.X += 1f;
            float inputLength = inputDirection.LengthSquared();
            if (inputLength > 0)
                inputDirection /= MathF.Sqrt(inputLength);
            Vector2 targetVelocity = inputDirection * speed;
            Vector3 velocity = body.Velocity.Linear;
            if (!UpdateTransforms)
                velocity = Vector3.Zero;
            float maxVelChange = (float)dt * acceleration;
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
            float velLen = targetVelocity.Length();
            if (prevFootstepTime + footstepInterval < footstepTime)
            {
                SCPCBPlayerEntity ent = this;
                Game.Simulation.RayCast(Position, -Vector3.UnitY, 5f, ref ent, 0);
                prevFootstepTime = footstepTime;
            }
            walkViewTimer += (velLen > 0f ? 1f : 0f) * viewBobSpeed * (float)dt;
            footstepTime += (float)dt * (velLen > 0f ? 1f : 0f);
            UpdateTransforms = !InputHandler.IsKeyPressed(Veldrid.Key.V);
            if (!UpdateTransforms)
            {
                QuaternionEx.Transform(Vector3.UnitX, Rotation, out Vector3 charSide);
                Position += (viewDirection * targetVelocity.Y + charSide * targetVelocity.X) * (float)dt * speed;
                MarkTransformDirty(TransformDirtyFlags.Position);
            }
            else
            {
                body.Velocity.Linear = velocity;
            }
        }
    }
}