using System.Threading.Tasks;
using Engine.Game;
using Engine.Game.Entities;

namespace Propnado
{
    public partial class Propnado2 : GameApp
    {
        public override string Name => "Propnado2";

        public Entity Player;

        public override void Tick(double dt)
        {
            if (Player == null)
            {
                Player = new NoClipPlayer();
            }
            base.Tick(dt);
        }
    }
}
