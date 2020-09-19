using System;
using System.Collections.Generic;
using System.Text;
using ECSRogue.Managers;

namespace ECSRogue.Components
{
    class LevelPosition : Component, IIndexableComponent
    {
        private int currentLevel;
        public int CurrentLevel
        {
            get => currentLevel;
            set
            {
                OnComponentUpdated(this, new ComponentUpdatedEventArgs(currentLevel, value, Id));
                currentLevel = value;
            }
        }

        public LevelPosition()
        {

        }

        public LevelPosition(LevelPosition levelPosition)
        {
            CurrentLevel = levelPosition.CurrentLevel;
        }

        public override object Clone()
        {
            return new LevelPosition(this);
        }

        public void OnComponentUpdated(object source, ComponentUpdatedEventArgs args)
        {
            EventHandler<ComponentUpdatedEventArgs> handler = ComponentUpdated;

            handler?.Invoke(source, args);
        }

        public event EventHandler<ComponentUpdatedEventArgs> ComponentUpdated;
        public object GetIndexValue()
        {
            return currentLevel;
        }
    }
}
