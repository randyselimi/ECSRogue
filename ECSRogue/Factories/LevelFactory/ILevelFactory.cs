using ECSRogue.Managers.Levels;

namespace ECSRogue.Factories.LevelFactory
{
    internal interface ILevelFactory
    {
        //Create some kind of creator to pass

        Level GenerateLevel(int maxWidth, int maxHeight);
    }
}