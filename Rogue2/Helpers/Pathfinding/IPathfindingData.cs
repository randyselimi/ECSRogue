using Microsoft.Xna.Framework;

namespace Rogue2.Helpers.Pathfinding
{
    public interface IPathfindingData
    {
        public int CalculateWeightValue(Vector2 Position);
    }
}