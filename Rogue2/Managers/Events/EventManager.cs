using Microsoft.Xna.Framework;
using Rogue2.Handlers.Events.EventProcessor;
using Rogue2.Managers.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Rogue2.Managers.Events

{
    //Defintley want to make a more robust system for event queue handling. TODO: Add methods for maniupulation of event queue and make eventQueue filtered as events are created
    class EventManager : IManager
    {
        public List<IEvent> eventQueue = new List<IEvent>();
        private List<IEventProcessor> eventProcessors = new List<IEventProcessor>();

        public EventManager(List<IEventProcessor> eventProcessors)
        {
            this.eventProcessors = eventProcessors;
        }

        public void Update(GameTime gameTime, int gameTurn, List<IEvent> eventQueue)
        {
            foreach (var eventProccesor in eventProcessors)
            {
                eventProccesor.Process(eventQueue);
            }
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
        public int lifetime { get; set; } = 0;
        public string eventType { get; set; }
        public List<Entity> entities { get; set; } = new List<Entity>();
        public List<object> arguments { get; set; } = new List<object>();

        public GameEvent(string eventType, List<Entity> entities, params object[] arguments)
        {
            this.eventType = eventType;
            this.entities = entities;
            this.arguments = arguments.ToList();
        }
    }

    public class UIEvent : IEvent
    {
        public int lifetime { get; set; }
        public string eventType { get; set; }
        public bool handled { get; set; }
        public List<object> arguments { get; set; } = new List<object>();
        public UIEvent(string eventType, params object[] arguments)
        {
            this.eventType = eventType;
            this.arguments = arguments.ToList();
        }
    }

    //Event regarding player input into game


}
