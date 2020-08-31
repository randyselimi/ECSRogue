using Microsoft.Xna.Framework;
using Rogue2.Components;
using Rogue2.Helpers.Pathfinding;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rogue2.Systems
{
    class MonsterAISystem : GameSystem
    {
        EntityManager entityManager;
        private MonsterAISystem()
        {
            entityFilter.AddComponentToFilter<Position>();
            entityFilter.AddComponentToFilter<MonsterAI>();
        }
        public MonsterAISystem(EntityManager entityManager) : this()
        {
            this.entityManager = entityManager;
        }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            Entity player = entityManager.GetEntitiesByComponent<Player>().FirstOrDefault();

            foreach (var monster in filteredEntities.Values)
            {
                if (monster.GetComponent<Turn>().takenTurn == false)
                {
                    monster.GetComponent<MonsterAI>().path = PathfindingAlgorithm.GetPath(monster.GetComponent<Position>().position, player.GetComponent<Position>().position, new MonsterAIPathfindingData(entityManager), 60);
                    monster.GetComponent<MonsterAI>().path.Pop();
                    if (monster.GetComponent<MonsterAI>().path.Count == 0)
                    {
                        monster.GetComponent<Turn>().takenTurn = true;

                        break;
                    }

                    monster.GetComponent<Position>().position = monster.GetComponent<MonsterAI>().path.Pop();

                    monster.GetComponent<Turn>().takenTurn = true;

                }
            }
        }
    }

    class MonsterAIPathfindingData : IPathfindingData
    {
        EntityManager entityManager;
        public MonsterAIPathfindingData(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        public int CalculateWeightValue(Vector2 position)
        {

            List<Entity> tiles = entityManager.GetEntitiesByComponent<Position>().Where(x => x.GetComponent<Position>().position == position).ToList();

            int gvalue = 0;

            if (tiles.Exists(x => x != null && x.HasComponent<Wall>()))
            {
                gvalue += 1000;
            }
            else if (tiles.Exists(x => x != null && x.HasComponent<Floor>()))
            {
                gvalue += 1;
            }
            else if (tiles.Exists(x => x != null && x.HasComponent<Door>()))
            {
                gvalue += 5;
            }
            else
            {
                gvalue += 10;
            }

            return gvalue;
        }
    }
}
