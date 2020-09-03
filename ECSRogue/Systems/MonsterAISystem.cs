using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Managers;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.Managers.Levels;
using Microsoft.Xna.Framework;
using Pathfinding;

namespace ECSRogue.Systems
{
    internal class MonsterAISystem : GameSystem
    {
        private readonly EntityManager _entityManager;
        private readonly LevelManager levelManager;
        private readonly Pathfinder pathfinder;
        public Entity player;

        private MonsterAISystem()
        {
        entityFilter.AddComponentToFilter<Position>();
            entityFilter.AddComponentToFilter<MonsterAI>();
        }

        public MonsterAISystem(EntityManager entityManager, LevelManager levelManager) : this()
        {
            this._entityManager = entityManager;
            this.levelManager = levelManager;
            pathfinder = new Pathfinder(CalculateMonsterWeightValue);
        }

        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            var currentLevel = levelManager.GetCurrentLevel();

            var x = 0;
            foreach (var monster in filteredEntities.Values)
            {
                x++;
                if (monster.GetComponent<Turn>().takenTurn == false)
                {
                    var monsterPosition = monster.GetComponent<Position>().position;
                    var playerPosition = player.GetComponent<Position>().position;

                    monster.GetComponent<MonsterAI>().path = pathfinder.GetPath((int) monsterPosition.X,
                        (int) monsterPosition.Y,
                        (int) playerPosition.X, (int) playerPosition.Y, currentLevel.nodeMap);

                    monster.GetComponent<MonsterAI>().path.Pop();

                    if (monster.GetComponent<MonsterAI>().path.Count == 0)
                    {
                        monster.GetComponent<Turn>().takenTurn = true;

                        continue;
                    }

                    var pop = monster.GetComponent<MonsterAI>().path.Pop();
                    var movement = new Vector2(pop[0] - monsterPosition.X, pop[1] - monsterPosition.Y);

                    if (monster.GetComponent<MonsterAI>().path.Count == 0)
                    {
                        if (movement == new Vector2(0, -1))
                            eventQueue.Add(new GameEvent("Attack_Up", new List<Entity> {monster}));
                        if (movement == new Vector2(0, 1))
                            eventQueue.Add(new GameEvent("Attack_Down", new List<Entity> {monster}));
                        if (movement == new Vector2(-1, 0))
                            eventQueue.Add(new GameEvent("Attack_Left", new List<Entity> {monster}));
                        if (movement == new Vector2(1, 0))
                            eventQueue.Add(new GameEvent("Attack_Right", new List<Entity> {monster}));
                    }

                    else
                    {
                        if (movement == new Vector2(0, -1))
                            eventQueue.Add(new GameEvent("Move_Up", new List<Entity> {monster}));
                        if (movement == new Vector2(0, 1))
                            eventQueue.Add(new GameEvent("Move_Down", new List<Entity> {monster}));
                        if (movement == new Vector2(-1, 0))
                            eventQueue.Add(new GameEvent("Move_Left", new List<Entity> {monster}));
                        if (movement == new Vector2(1, 0))
                            eventQueue.Add(new GameEvent("Move_Right", new List<Entity> {monster}));
                    }
                }
            }
        }

        //Temporary implmentaiot
        public float CalculateMonsterWeightValue(int x, int y)
        {
            var position = new Vector2(x, y);

            var tiles = _entityManager.indexManager.GetIndice<Position, PositionIndex>().GetEntityByIndex(position).Values
                .ToList();

            float weight = 0;

            if (tiles.Exists(x => x != null && x.HasComponent<Collideable>())) weight = float.PositiveInfinity;
            if (tiles.Exists(x => x != null && x.HasComponent<Door>())) weight = 1;

            return weight;
        }
    }
}