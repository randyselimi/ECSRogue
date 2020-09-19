using ECSRogue.Managers.Levels;
using ECSRogue.Partis;

namespace ECSRogue.Factories.LevelFactory
{
    public interface ILevelFactory
    {
        //Create some kind of creator to pass

        Level GenerateLevel(int levelId, int maxWidth, int maxHeight, PartisInstance instance);
    }
}