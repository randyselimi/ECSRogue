using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Managers;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.Managers.Levels;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;
using Pathfinding;

namespace ECSRogue.Systems
{
    internal class MonsterAISystem : GameSystem
    {
        private readonly LevelManager levelManager;
        private Pathfinder pathfinder;

        public MonsterAISystem(LevelManager levelManager) : base(SystemTypes.UpdateSystem)
        {
            this.levelManager = levelManager;
        }

        public override void Update(PartisInstance instance)
        {
            var currentLevel = levelManager.GetCurrentLevel();

            var player = instance.GetEntitiesByIndex(new TypeIndexer(typeof(Player))).Single();
            var playerPosition = player.GetComponent<Position>().position;


            if (pathfinder == null)
            {
                pathfinder = new Pathfinder(CalculateMonsterWeightValue);
            }

            foreach (var monsterEntity in instance.GetEntitiesByIndex(new TypeIndexer(typeof(MonsterAI))))
            {
                //If the monster hasn't taken its turn and is on the same level as the player, try to find a path to them
                if (monsterEntity.GetComponent<Turn>().takenTurn == false && monsterEntity.GetComponent<LevelPosition>().CurrentLevel == currentLevel.Id)
                {
                    var monsterPosition = monsterEntity.GetComponent<Position>().position;
                    
                    monsterEntity.GetComponent<MonsterAI>().path = pathfinder.GetPath((int) monsterPosition.X,
                        (int) monsterPosition.Y,
                        (int) playerPosition.X, (int) playerPosition.Y, currentLevel.nodeMap);

                    //First tile in path is tile occupied by monsterEntity. Not needed so pop it off
                    monsterEntity.GetComponent<MonsterAI>().path.Pop();

                    //If the monster for some reason is pathing to itself, make it wait
                    if (monsterEntity.GetComponent<MonsterAI>().path.Count == 0)
                    {
                        monsterEntity.GetComponent<Turn>().takenTurn = true;
                        continue;
                    }

                    //TODO this is ugly and should be refactored
                    var pop = monsterEntity.GetComponent<MonsterAI>().path.Pop();
                    var movement = new Vector2(pop[0] - monsterPosition.X, pop[1] - monsterPosition.Y);

                    //Temporary but if the tile is occupied by the player, then instead of moving, send an attackEvent
                    if (instance.GetEntitiesByIndexes(new PositionIndexer(new Vector2(pop[0], pop[1])), new TypeIndexer(typeof(Player))).Count != 0)
                    {
                        instance.AddEvent(new AttackEvent(monsterEntity, monsterEntity.GetComponent<Position>().position + movement, monsterEntity.GetComponent<LevelPosition>().CurrentLevel));
                    }

                    //Otherwise just send a moveEvent
                    else
                    {
                        instance.AddEvent(new MoveEvent(monsterEntity, movement));
                    }
                }
                else
                {
                    monsterEntity.GetComponent<Turn>().takenTurn = true;
                }
            }

            float CalculateMonsterWeightValue(int x, int y)
            {
                var position = new Vector2(x, y);

                var tiles = instance.GetEntitiesByIndexes(new PositionIndexer(position), new LevelIndexer(levelManager.GetCurrentLevel().Id))
                    .ToList();

                float weight = 0;

                if (tiles.Exists(x => x != null && x.HasComponent<Collideable>())) weight = float.PositiveInfinity;
                if (tiles.Exists(x => x != null && x.HasComponent<MonsterAI>())) weight = 1000;
                if (tiles.Exists(x => x != null && x.HasComponent<Door>())) weight = 1;

                return weight;
            }
        }


    }
}