using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.Trees;
using Engine.Game.Physics;

namespace VirtualBright.Util
{
    public class AStarNode
    {
        public Vector3 Position;
        public float Cost;
        public List<AStarNode> Connected = new List<AStarNode>();

        public AStarNode()
        {
        }

        public AStarNode(Vector3 pos)
        {
            Position = pos;
            Cost = 1f;
        }
    }

    public class AStar : IRayHitHandler
    {
        public List<AStarNode> Map = new List<AStarNode>();
        public Func<AStarNode, AStarNode, float> StockH = (a, b) => a == null || b == null ? 0f : Vector3.Distance(a.Position, b.Position);
        private Simulation sim;
        private int currentBuilderNode;
        private bool foundRayTarget;

        public AStar(Simulation simulation)
        {
            sim = simulation;
        }

        public void RebuildMap()
        {
            for (int i = 0; i < Map.Count; i++)
            {
                Map[i].Connected.Clear();
            }
            IRayHitHandler handler = this;
            for (int i = 0; i < Map.Count; i++)
            {
                for (int j = 0; j < Map.Count; j++)
                {
                    if (i == j)
                        continue;
                    currentBuilderNode = j;
                    foundRayTarget = false;
                    sim.RayCast(Map[i].Position, Vector3.Normalize(Map[j].Position - Map[i].Position), Vector3.Distance(Map[i].Position, Map[j].Position), ref handler, i);
                    if (foundRayTarget)
                        continue;
                    Map[i].Connected.Add(Map[j]);
                }
            }
        }

        public Vector3[] SimpleGetPath(Vector3 start, Vector3 end)
        {
            return GetPath(GetNode(start), GetNode(end), StockH);
        }

        public AStarNode GetNode(Vector3 pos)
        {
            float dist = float.MaxValue;
            AStarNode ent = null;
            foreach (var item in Map)
            {
                float d = Vector3.Distance(pos, item.Position);
                if (dist > d || ent == null)
                {
                    dist = d;
                    ent = item;
                }
            }
            return ent;
        }

        public Vector3[] GetPath(AStarNode start, AStarNode end, Func<AStarNode, AStarNode, float> h)
        {
            if (start == null || end == null)
                return new Vector3[0];
            Dictionary<AStarNode, float> queue = new Dictionary<AStarNode, float>();
            queue.Add(start, 0f);
            Dictionary<AStarNode, AStarNode> cameFrom = new Dictionary<AStarNode, AStarNode>();
            Dictionary<AStarNode, float> costSoFar = new Dictionary<AStarNode, float>();
            cameFrom.Add(start, null);
            costSoFar.Add(start, 0f);
            AStarNode current = null;
            int count = 0;
            while (count < 10000 && queue.Count != 0)
            {
                count++;
                current = queue.OrderByDescending(x => x.Value).FirstOrDefault().Key;
                queue.Remove(current);

                if (current == end)
                {
                    break;
                }

                bool found = false;
                foreach (var next in current.Connected)
                {
                    float newCost = costSoFar[current] + h(current, next);
                    if ((!cameFrom.ContainsKey(next)) || newCost < costSoFar[next])
                    {
                        if (costSoFar.ContainsKey(next))
                            costSoFar[next] = newCost;
                        else
                            costSoFar.Add(next, newCost);
                        float priority = 0f + h(next, end);
                        if (queue.ContainsKey(next))
                            queue[next] = priority;
                        else
                            queue.Add(next, priority);
                        if (cameFrom.ContainsKey(next))
                            cameFrom[next] = current;
                        else
                            cameFrom.Add(next, current);
                        found = true;
                    }
                }
                /*if (!found)
                {
                    var prev = cameFrom[current];
                    if (prev != null)
                    {
                        if (queue.ContainsKey(prev))
                            queue[prev] = float.MaxValue;
                        else
                            queue.Add(prev, float.MaxValue);
                    }
                }*/
            }
            List<Vector3> path = new List<Vector3>();
            path.Add(current.Position);
            AStarNode cur = cameFrom[current];
            while (cur != null && cameFrom.ContainsKey(cur))
            {
                path.Add(cur.Position);
                cur = cameFrom[cur];
            }
            return path.ToArray();
        }

        public bool AllowTest(CollidableReference collidable)
        {
            return collidable.Mobility == CollidableMobility.Static;
        }

        public bool AllowTest(CollidableReference collidable, int childIndex)
        {
            return collidable.Mobility == CollidableMobility.Static;
        }

        public void OnRayHit(in RayData ray, ref float maximumT, float t, Vector3 normal, CollidableReference collidable, int childIndex)
        {
            foundRayTarget = true;
        }
    }
}