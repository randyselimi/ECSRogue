using ECSRogue.Managers.Levels;
using ECSRogue.Partis;

namespace ECSRogue.Factories.LevelFactory
{
    internal interface ILevelFactory
    {
        //Create some kind of creator to pass

        Level GenerateLevel(int maxWidth, int maxHeight, PartisInstance instance);
    }
}