using System.Collections.Generic;
using System.Linq;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events.EventProcessor;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers.Events

{
    //Defintley want to make a more robust system for event queue handling. TODO: Add methods for maniupulation of event queue and make eventQueue filtered as events are created
    public class EventManager
    {
        private readonly List<IEventProcessor> eventProcessors = new List<IEventProcessor>();
        public List<IEvent> eventQueue = new List<IEvent>();

        public EventManager(List<IEventProcessor> eventProcessors)
        {
            this.eventProcessors = eventProcessors;
        }

        //This shouldn't be a performance bottleneck since the eventsystem shouldn't be handling that many events
        public List<T> GetEvents<T>() where T : IEvent
        {
            return eventQueue.OfType<T>().ToList();
        }

        public void AddEvent(IEvent newEvent)
        {
            eventQueue.Add(newEvent);
        }

        public void RemoveEvent(IEvent @event)
        {
            eventQueue.Remove(@event);
        }

        public void Update()
        {
            foreach (var eventProcessor in eventProcessors) eventProcessor.Process(this);
        }


    }

    //Used to couple systems together
    public interface IEvent
    {
        //public int lifetime { get; set; }
    }

    //Event regarding game logic, entities, and systems
    public class GameEvent : IEvent
    {
        public int lifetime;
        public GameEvents EventType { get; }

        protected GameEvent(GameEvents eventType)
        {
            EventType = eventType;
        }

        //public int lifetime { get; set; } = 0;
    }

    //probably should split into mouse events and keyboard events
    public class UiEvent : IEvent
    {
        public UiEvent(UiEvents eventType, params object[] arguments)
        {
            this.EventType = eventType;
            this.arguments = arguments.ToList();
        }

        public bool handled { get; set; }
        public List<object> arguments { get; set; } = new List<object>();
        public int lifetime { get; set; }
        public UiEvents EventType { get; set; }
    }

    //Event regarding player input into game
    public enum GameEvents
    {
        Attack,
        Pickup,
        Move,
        Collide,
        Damage,
        Equip,
        LevelChange
    }

    public enum UiEvents
    {
        Inventory,
        LeftMouseClick,
        Exit
    }

    /// <summary>
    /// Defines an event in which an Entity makes an attack action on a target given by Position.
    /// </summary>
    public class AttackEvent : GameEvent
    {
        public AttackEvent(Entity attackingEntity, Vector2 attackingPosition, int attackingLevel) : base(GameEvents.Attack)
        {
            AttackingEntity = attackingEntity;
            AttackingPosition = attackingPosition;
            AttackingLevel = attackingLevel;
        }
        public Entity AttackingEntity { get; set; }
        public Vector2 AttackingPosition { get; set; }
        public int AttackingLevel { get; set; }
    }

    /// <summary>
    /// Defines an event in which an Entity receives damage 
    /// </summary>
    public class DamageEvent : GameEvent
    {
        public DamageEvent(Entity damagedEntity, int damageAmount) : base(GameEvents.Damage)
        {
            DamagedEntity = damagedEntity;
            DamageAmount = damageAmount;
        }
        public Entity DamagedEntity { get; set; }
        public int DamageAmount { get; set; }
    }

    internal class LevelChangeEvent : GameEvent
    {
        public LevelChangeEvent(Entity levelChangingEntity, int changeAmount) : base(GameEvents.LevelChange)
        {
            LevelChangingEntity = levelChangingEntity;
            ChangeAmount = changeAmount;
        }
        public Entity LevelChangingEntity { get; set; }
        public int ChangeAmount { get; set; }
    }

    /// <summary>
    /// Defines an event in which an Entity attempts to move to another tile
    /// </summary>
    public class MoveEvent : GameEvent
    {
        public MoveEvent(Entity movingEntity, Vector2 movingPosition) : base(GameEvents.Move)
        {
            MovingEntity = movingEntity;
            MovingPosition = movingPosition;
        }
        public Entity MovingEntity { get; set; }
        public Vector2 MovingPosition { get; set; }
    }

    public class PickupEvent : GameEvent
    {
        public PickupEvent(Entity pickingEntity) : base(GameEvents.Pickup)
        {
            PickingEntity = pickingEntity;
        }

        public Entity PickingEntity { get; set; }
    }

    /// <summary>
    /// Defines an event in which a moving Entity has collided with another Entity
    /// </summary>
    public class CollisionEvent : GameEvent
    {
        public CollisionEvent(Entity movingEntity, Entity collidedEntity) : base(GameEvents.Collide)
        {
            MovingEntity = movingEntity;
            CollidedEntity = collidedEntity;
        }

        public Entity MovingEntity { get; set; }
        public Entity CollidedEntity { get; set; }
    }

    /// <summary>
/// Defines an event in which an entity attempts to equip another
    /// </summary>
    public class EquipEvent : GameEvent
    {
        public EquipEvent(Entity equippingEntity, Entity equippedEntity) : base(GameEvents.Equip)
        {
            EquippingEntity = equippingEntity;
            EquippedEntity = equippedEntity;

        }
        public Entity EquippingEntity { get; set; }
        public Entity EquippedEntity { get; set; }
    }
}