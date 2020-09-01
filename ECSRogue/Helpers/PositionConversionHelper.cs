using Microsoft.Xna.Framework;

namespace ECSRogue.Helpers
{
    internal static class PositionConversionHelper
    {
        private static readonly int tileToRenderConversionFactor = 50;

        public static Vector2 tileToRender(Vector2 tilePosition)
        {
            return Vector2.Multiply(tilePosition, tileToRenderConversionFactor);
        }
    }
}