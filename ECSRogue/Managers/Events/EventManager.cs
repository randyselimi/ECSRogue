using System.Collections.Generic;
using System.Linq;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events.EventProcessor;
using Microsoft.Xna.Framework;

namespace ECSRogue.Managers.Events

{
    //Defintley want to make a more robust system for event queue handling. TODO: Add methods for maniupulation of event queue and make eventQueue filtered as events are created
    internal class EventManager : IManager
    {
        private readonly List<IEventProcessor> eventProcessors = new List<IEventProcessor>();
        public List<IEvent> eventQueue = new List<IEvent>();

        public EventManager(List<IEventProcessor> eventProcessors)
        {
            this.eventProcessors = eventProcessors;
        }

        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
            foreach (var eventProccesor in eventProcessors) eventProccesor.Process(eventQueue);
        }
    }

    //Used to couple systems together
    public interface IEvent
    {
        public int lifetime { get; set; }
    }

    //Event regarding game logic, entities, and systems
    public class GameEvent : IEvent
    {
        public GameEvent(string eventType, List<Entity> entities, params object[] arguments)
        {
            this.eventType = eventType;
            this.entities = entities;
            this.arguments = arguments.ToList();
        }

        public string eventType { get; set; }
        public List<Entity> entities { get; set; } = new List<Entity>();
        public List<object> arguments { get; set; } = new List<object>();
        public int lifetime { get; set; } = 0;
    }

    public class UIEvent : IEvent
    {
        public UIEvent(string eventType, params object[] arguments)
        {
            this.eventType = eventType;
            this.arguments = arguments.ToList();
        }

        public string eventType { get; set; }
        public bool handled { get; set; }
        public List<object> arguments { get; set; } = new List<object>();
        public int lifetime { get; set; }
    }

    //Event regarding player input into game
}