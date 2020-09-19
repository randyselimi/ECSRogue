using System.Linq;
using ECSRogue.Components;
using ECSRogue.Managers;
using ECSRogue.Managers.Events;
using ECSRogue.Managers.Levels;
using ECSRogue.Partis;

namespace ECSRogue.Systems
{
    public class LevelChangeSystem : GameSystem
    {
        private LevelManager levelManager;
        public LevelChangeSystem(LevelManager levelManager) : base(SystemTypes.UpdateSystem)
        {
            this.levelManager = levelManager;
        }
        public override void Update(PartisInstance instance)
        {
            foreach (var gameEvent in instance.GetEvents<GameEvent>())
            {
                if (gameEvent.EventType == GameEvents.LevelChange)
                {
                    var levelChangeEvent = gameEvent as LevelChangeEvent;

                    levelChangeEvent.LevelChangingEntity.GetComponent<LevelPosition>().CurrentLevel +=
                        levelChangeEvent.ChangeAmount;

                    if (levelChangeEvent.LevelChangingEntity == instance.GetEntitiesByIndex(new TypeIndexer(typeof(Player))).Single())
                    {
                        levelManager.ChangeCurrentLevel(
                            levelChangeEvent.LevelChangingEntity.GetComponent<LevelPosition>().CurrentLevel, instance);
                    }

                }
            }

        }

    }
}