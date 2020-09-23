using System.Collections.Generic;
using System.Linq;
using ECSRogue.Components;
using ECSRogue.Managers;
using ECSRogue.Managers.Entities;
using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ECSRogue.Handlers.Input
{
    internal class InputHandler
    {
        private KeyboardState currentKeyboardState;
        private MouseState currentMouseState;

        public Keys? modifierKey;
        //double inputDelay = 100;

        private KeyboardState previousKeyboardState;

        private MouseState previousMouseState;

        private double timeSinceLastUpdate;

        private UiEvent uiEvent;

        /// <summary>
        ///     Queries mouse and keyboard for current input state. First checks if input is a valid input for UI Layer. If so,
        ///     sends input to UI through global event queue. If input is not handled by UI layer or input
        ///     is not valid on UI layer, then sends input to game layer.
        ///     TODO: might need to save current and previous input state if uievent is sent for second iteration since input might
        ///     change in that timeframe.
        /// </summary>
        /// <param name="gameTime"> global time variable </param>
        /// <param name="eventQueue"> global event queue </param>
        public void Update(GameTime gameTime, PartisInstance instance)
        {
            var player = instance.GetEntitiesByIndex(new TypeIndexer(typeof(Player))).SingleOrDefault();
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();

            uiEvent = ProcessInputUILayer(instance);

            //TODO roll the input query into a function
            //if there is no uiEvent (IE input not a valid ui input) or the input was not handled by the UI Layer (IE mouse click not on a button) then handle input as a game input
            if (uiEvent == null || uiEvent.handled == false)
            {
                //if there is no modifier, then handle wasd input as movement
                if (modifierKey == null)
                {
                    if (currentKeyboardState.IsKeyDown(Keys.A) && previousKeyboardState.IsKeyUp(Keys.A))
                    {
                        instance.AddEvent(new MoveEvent(player, new Vector2(-1, 0)));
                        timeSinceLastUpdate = 0;
                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.D) && previousKeyboardState.IsKeyUp(Keys.D))
                    {
                        instance.AddEvent(new MoveEvent(player, new Vector2(1, 0)));

                        timeSinceLastUpdate = 0;
                    }

                    else if (currentKeyboardState.IsKeyDown(Keys.W) && previousKeyboardState.IsKeyUp(Keys.W))
                    {
                        instance.AddEvent(new MoveEvent(player, new Vector2(0, -1)));

                        timeSinceLastUpdate = 0;
                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.S) && previousKeyboardState.IsKeyUp(Keys.S))
                    {
                        instance.AddEvent(new MoveEvent(player, new Vector2(0, 1)));

                        timeSinceLastUpdate = 0;
                    }
                }

                if (currentKeyboardState.IsKeyDown(Keys.P) && previousKeyboardState.IsKeyUp(Keys.P))
                {
                    instance.AddEvent(new PickupEvent(player));

                    timeSinceLastUpdate = 0;
                }

                //TODO make modifier input not based on conditionals. IE if T is not a conditional then modifiderkey = T
                if (currentKeyboardState.IsKeyDown(Keys.T))
                {
                    modifierKey = Keys.T;

                    timeSinceLastUpdate = 0;
                }

                if (modifierKey == Keys.T)
                {
                    if (currentKeyboardState.IsKeyDown(Keys.A) && previousKeyboardState.IsKeyUp(Keys.A))
                    {
                        instance.AddEvent(new AttackEvent(player, player.GetComponent<Position>().position + new Vector2(-1, 0), player.GetComponent<LevelPosition>().CurrentLevel));
                        timeSinceLastUpdate = 0;

                        modifierKey = null;
                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.D) && previousKeyboardState.IsKeyUp(Keys.D))
                    {
                        instance.AddEvent(new AttackEvent(player, player.GetComponent<Position>().position + new Vector2(1, 0), player.GetComponent<LevelPosition>().CurrentLevel));

                        timeSinceLastUpdate = 0;

                        modifierKey = null;
                    }

                    else if (currentKeyboardState.IsKeyDown(Keys.W) && previousKeyboardState.IsKeyUp(Keys.W))
                    {
                        instance.AddEvent(new AttackEvent(player, player.GetComponent<Position>().position + new Vector2(0, -1), player.GetComponent<LevelPosition>().CurrentLevel));

                        timeSinceLastUpdate = 0;

                        modifierKey = null;
                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.S) && previousKeyboardState.IsKeyUp(Keys.S))
                    {
                        instance.AddEvent(new AttackEvent(player, player.GetComponent<Position>().position + new Vector2(0, 1), player.GetComponent<LevelPosition>().CurrentLevel));

                        timeSinceLastUpdate = 0;

                        modifierKey = null;
                    }
                }

                if (currentKeyboardState.IsKeyDown(Keys.M) && previousKeyboardState.IsKeyUp(Keys.M))
                {
                    instance.AddEvent(new LevelChangeEvent(player, 1));
                }

                if (currentKeyboardState.IsKeyDown(Keys.N) && previousKeyboardState.IsKeyUp(Keys.N))
                {
                    instance.AddEvent(new LevelChangeEvent(player, -1));

                }
            }

            //Assign previous input state to current state
            previousMouseState = currentMouseState;
            previousKeyboardState = currentKeyboardState;

            //If there is no input, increase timeSinceLastUpdate by elapsed time
            timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public bool IsMouseInput(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed) return true;

            if (mouseState.RightButton == ButtonState.Pressed) return true;

            if (mouseState.MiddleButton == ButtonState.Pressed) return true;

            return false;
        }

        public bool IsKeyboardInput(KeyboardState keyboardState)
        {
            if (keyboardState.GetPressedKeys().Length != 0) return true;

            return false;
        }

        public UiEvent ProcessInputUILayer(PartisInstance instance)
        {
            UiEvent uiEvent = null;

            if (currentMouseState.LeftButton == ButtonState.Pressed &&
                previousMouseState.LeftButton == ButtonState.Released)
                uiEvent = new UiEvent(UiEvents.LeftMouseClick, currentMouseState.X, currentMouseState.Y);

            else if (currentKeyboardState.IsKeyDown(Keys.I) && previousKeyboardState.IsKeyUp(Keys.I))
                uiEvent = new UiEvent(UiEvents.Inventory);

            else if (currentKeyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
                uiEvent = new UiEvent(UiEvents.Exit);

            if (uiEvent != null) instance.AddEvent(uiEvent);

            return uiEvent;
        }
    }


}