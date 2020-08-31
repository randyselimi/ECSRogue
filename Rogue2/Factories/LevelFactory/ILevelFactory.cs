using Rogue2.Managers.Levels;

namespace Rogue2.Managers
{
    interface ILevelFactory
    {
        //Create some kind of creator to pass

        Level GenerateLevel(int maxWidth, int maxHeight);


    }
}
