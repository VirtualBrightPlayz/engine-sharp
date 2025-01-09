using System.Linq;
using Engine.Assets;

namespace GameSrc
{
    public static class ResourceManagerExtensions
    {
        public static RMeshModel LoadRoomModel(string path)
        {
            RMeshModel buf = new RMeshModel(path);
            if (buf != null)
                return buf;
            buf = new RMeshModel(path);
            return buf;
        }
    }
}