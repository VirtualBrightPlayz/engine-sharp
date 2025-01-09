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
using ImGuiNET;

namespace GameSrc
{
    public class SCPCBPlayerEntity : PhysicsEntity
    {
        public InputHandler InputHandler => MiscGlobals.GameInputHandler;
        public Capsule shape;
        public float speed = 5f;
        public float acceleration = 100f;
        public Vector3 viewDirection = -Vector3.UnitZ;
        public Vector3 viewDirectionUp = Vector3.UnitY;
        public Vector3 direction = -Vector3.UnitZ;
        public Vector2 lookAxis = Vector2.Zero;
        public Quaternion CameraRotation = Quaternion.Identity;
        public Vector3 desiredVelocity = Vector3.Zero;
        private bool wasEscPressed = false;
        private float walkViewTimer;
        // private float viewBobSpeed = 0.03f;
        // private float viewBobAmount = 0.1f;
        private float viewBobSpeed = 7.5f;
        private float viewBobAmount = 0.075f;
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
        private Vector4 nextLightColor = Vector4.One;
        private bool posFlipFlop;
        private double posDeltaFlipFlop;

        public SCPCBPlayerEntity() : base("Player")
        {
            footstepSource = new AudioSource("PlayerFootsteps");
            footstepSource.MinGain = 1f;
            footstepSource.MaxGain = 1f;
            shape = new Capsule(0.4f, 1.4f);
            shapeIndex = Game.Simulation.Shapes.Add(shape);
            var inertia = shape.ComputeInertia(1f);
            bodyHandle = Game.Simulation.Bodies.Add(BodyDescription.CreateDynamic(Position, inertia, new CollidableDescription(shapeIndex.Value, 0.1f, float.MaxValue, ContinuousDetection.Discrete), shape.Radius * 0.02f));
        }

        public override void PreDraw(Renderer renderer, double dt)
        {
            base.PreDraw(renderer, dt);
            UpdateLook(dt);
            Vector3 viewPos = Position + Vector3.UnitY * shape.HalfLength + Vector3.UnitY * upDownBob;
            QuaternionEx.Transform(LocalUp, Quaternion.CreateFromAxisAngle(viewDirection, leftRightBob * (MathF.PI / 180f)), out Vector3 up);
            renderer.ViewMatrix = Matrix4x4.CreateLookAt(viewPos, viewPos + viewDirection, up);
            // Renderer.Current.WorldInfoResource.UploadData(Renderer.Current, new Vector4(viewPos, 1f));
            footstepSource.Position = viewPos;
            renderer.ViewPosition = viewPos;
            // ForwardConsts.LightPosition = viewPos - Vector3.UnitY;
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
            BodyReference body = Game.Simulation.Bodies[bodyHandle.Value];
            if (ImGui.Begin("Test"))
            {
                ImGui.Text("Test Window");
                ImGui.Text($"Position {Position.ToString()}");
                ImGui.Text($"Velocity {body.Velocity.Linear.ToString()}");
                ImGui.InputFloat("ViewBobSpeed", ref viewBobSpeed);
                ImGui.InputFloat("ViewBobAmount", ref viewBobAmount);
                ImGui.InputFloat("FootstepInterval", ref footstepInterval);
                ImGui.ColorEdit4("AmbientColor", ref ForwardConsts.AmbientColor, ImGuiColorEditFlags.Float);
                ImGui.ColorEdit4("LightColor", ref nextLightColor, ImGuiColorEditFlags.Float);
                ImGui.InputInt("MaxLights", ref ForwardConsts.MaxRealtimeLights);
                // ImGui.SliderFloat4("AmbientColor", ref ForwardConsts.AmbientLight, 0f, 1f);
                /*
                if (ImGui.Button("New light"))
                {
                    ForwardConsts.Lights.Add(new ForwardConsts.ForwardLight()
                    {
                        Position = Position,
                        Color = nextLightColor,
                        Range = 5f,
                    });
                }
                if (ImGui.Button("Clear lights"))
                {
                    ForwardConsts.Lights.Clear();
                }
                */
                ImGui.End();
            }
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
                /*
                if (posFlipFlop)
                {
                    InputHandler.Position = new Vector2(RenderingGlobals.Window.Width / 2, RenderingGlobals.Window.Height / 2);
                }
                else
                {
                    lookAxis += InputHandler.MouseDelta * Vector2.One * (float)delta * 100f;
                    lookAxis.Y = MathUtils.Clamp(lookAxis.Y, -89f, 89f);
                }
                posFlipFlop = !posFlipFlop;
                */
                // if (posDeltaFlipFlop > 0.025d)
                {
                    posDeltaFlipFlop = 0d;
                    InputHandler.Position = new Vector2(RenderingGlobals.Window.Width / 2, RenderingGlobals.Window.Height / 2);
                }
                // else
                {
                    lookAxis += InputHandler.MouseDelta * Vector2.One * 0.25f;
                    lookAxis.Y = MathUtils.Clamp(lookAxis.Y, -89f, 89f);
                }
                posDeltaFlipFlop += delta;
                Quaternion cameraRot = Quaternion.CreateFromYawPitchRoll(lookAxis.X * (MathF.PI / 180f), lookAxis.Y * (MathF.PI / 180f), 0f);
                CameraRotation = cameraRot;
                Rotation = Quaternion.CreateFromYawPitchRoll(lookAxis.X * (MathF.PI / 180f), 0f, 0f);
                QuaternionEx.Transform(-Vector3.UnitZ, cameraRot, out viewDirection);
                QuaternionEx.Transform(Vector3.UnitY, cameraRot, out viewDirectionUp);
                QuaternionEx.Transform(-Vector3.UnitZ, Rotation, out direction);
            }
            MarkTransformDirty(TransformDirtyFlags.Rotation);
            if (InputHandler.IsMouseDown(Veldrid.MouseButton.Right) && !wasEscPressed)
            {
                InputHandler.IsMouseLocked = !InputHandler.IsMouseLocked;
            }
            wasEscPressed = InputHandler.IsMouseDown(Veldrid.MouseButton.Right);
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
                footstepSource.SetBuffer(footstepClips[Random.Shared.Next(footstepClips.Length)]);
                footstepSource.Play();
                prevFootstepTime = footstepTime;
            }
            walkViewTimer += (velLen > 0f ? 1f : 0f) * viewBobSpeed * (float)dt;
            footstepTime += (float)dt * (velLen > 0f ? 1f : 0f);
            UpdateTransforms = !InputHandler.IsKeyPressed(Veldrid.Key.N);
            if (!UpdateTransforms)
            {
                Position += velocity * (float)dt * speed;
                MarkTransformDirty(TransformDirtyFlags.Position);
                // body.Pose.Position = Position;
            }
            else
            {
                body.Velocity.Linear = velocity;
            }
        }
    }
}