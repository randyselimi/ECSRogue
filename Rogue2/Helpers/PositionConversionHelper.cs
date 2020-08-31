using Microsoft.Xna.Framework;

namespace Rogue2.Helpers
{
    static class PositionConversionHelper
    {
        static int tileToRenderConversionFactor = 50;

        public static Vector2 tileToRender(Vector2 tilePosition)
        {
            return Vector2.Multiply(tilePosition, tileToRenderConversionFactor);
        }
    }
}
