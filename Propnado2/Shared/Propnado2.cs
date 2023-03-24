using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Skia;
using DefaultEcs;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using Engine.Game;
using Engine.Game.Components;
using FontStashSharp;
using Myra;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Styles;
using TheArtOfDev.HtmlRenderer.Avalonia;

namespace Propnado
{
    public partial class Propnado2 : GameApp
    {
        public override string Name => "Propnado2";

        // public Entity Player;
        private Texture2D tex;

        public override void Setup()
        {
            base.Setup();
            /*
            SkiaPlatform.Initialize();
            Bitmap bitmap = HtmlRender.RenderToImage(File.ReadAllText("index.html"), new Size(RenderingGlobals.ViewSize.X, RenderingGlobals.ViewSize.Y));
            {
                bitmap.Save("test.png");
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms);
                    byte[] arr = ms.ToArray();
                    tex = new Texture2D("UI", arr);
                }
            }
            */
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
            // Renderer.Current.Blit(tex);
        }

        public override void Tick(double dt)
        {
            // if (Player == null)
            {
                // Player = new NoClipPlayer();
                // Entities.Add(Player);
            }
            base.Tick(dt);
        }
    }
}
