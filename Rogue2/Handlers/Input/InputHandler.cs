using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rogue2.Managers.Entities;
using Rogue2.Managers.Events;
using System.Collections.Generic;

namespace Rogue2.Handlers
{
    class InputHandler : Handler
    {

        double timeSinceLastUpdate = 0;
        //double inputDelay = 100;

        public List<Entity> player = new List<Entity>();

        public Keys? modifierKey = null;

        MouseState previousMouseState;
        MouseState currentMouseState;

        KeyboardState previousKeyboardState;
        KeyboardState currentKeyboardState;

        UIEvent uiEvent;

        public InputHandler()
        {
        }

        /// <summary>
        /// 
        /// Queries mouse and keyboard for current input state. First checks if input is a valid input for UI Layer. If so, sends input to UI through global event queue. If input is not handled by UI layer or input
        /// is not valid on UI layer, then sends input to game layer.
        /// TODO: might need to save current and previous input state if uievent is sent for second iteration since input might change in that timeframe.
        /// </summary>
        /// <param name="gameTime"> global time variable </param>
        /// <param name="eventQueue"> global event queue </param>
        public override void Update(GameTime gameTime, List<IEvent> eventQueue)
        {
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();

            eventQueue.Add(new UIEvent("Debug_Text", "Previous Mouse State Is Left Click Pressed: " + (previousMouseState.LeftButton == ButtonState.Pressed), "Current Mouse State Is Left Click Pressed: " + (currentMouseState.LeftButton == ButtonState.Pressed)));

            uiEvent = ProcessInputUILayer(eventQueue);

            //if there is no uiEvent (IE input not a valid ui input) or the input was not handled by the UI Layer (IE mouse click not on a button) then handle input as a game input
            if (uiEvent == null || uiEvent.handled == false)
            {
                //if there is no modifier, then handle wasd input as movement
                if (modifierKey == null)
                {
                    if (currentKeyboardState.IsKeyDown(Keys.A) && previousKeyboardState.IsKeyUp(Keys.A))
                    {
                        eventQueue.Add(new GameEvent("Move_Left", player));
                        timeSinceLastUpdate = 0;
                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.D) && previousKeyboardState.IsKeyUp(Keys.D))
                    {
                        eventQueue.Add(new GameEvent("Move_Right", player));

                        timeSinceLastUpdate = 0;
                    }

                    else if (currentKeyboardState.IsKeyDown(Keys.W) && previousKeyboardState.IsKeyUp(Keys.W))
                    {
                        eventQueue.Add(new GameEvent("Move_Up", player));

                        timeSinceLastUpdate = 0;
                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.S) && previousKeyboardState.IsKeyUp(Keys.S))
                    {
                        eventQueue.Add(new GameEvent("Move_Down", player));

                        timeSinceLastUpdate = 0;
                    }
                }

                if (currentKeyboardState.IsKeyUp(Keys.P) && previousKeyboardState.IsKeyDown(Keys.P))
                {
                    eventQueue.Add(new GameEvent("Pickup", player));

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
                        eventQueue.Add(new GameEvent("Attack_Left", player));
                        timeSinceLastUpdate = 0;

                        modifierKey = null;
                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.D) && previousKeyboardState.IsKeyUp(Keys.D))
                    {
                        eventQueue.Add(new GameEvent("Attack_Right", player));

                        timeSinceLastUpdate = 0;

                        modifierKey = null;
                    }

                    else if (currentKeyboardState.IsKeyDown(Keys.W) && previousKeyboardState.IsKeyUp(Keys.W))
                    {
                        eventQueue.Add(new GameEvent("Attack_Up", player));

                        timeSinceLastUpdate = 0;

                        modifierKey = null;

                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.S) && previousKeyboardState.IsKeyUp(Keys.S))
                    {
                        eventQueue.Add(new GameEvent("Attack_Down", player));

                        timeSinceLastUpdate = 0;

                        modifierKey = null;
                    }
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
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                return true;
            }

            if (mouseState.MiddleButton == ButtonState.Pressed)
            {
                return true;
            }

            return false;
        }

        public bool IsKeyboardInput(KeyboardState keyboardState)
        {
            if (keyboardState.GetPressedKeys().Length != 0)
            {
                return true;
            }

            return false;
        }

        public UIEvent ProcessInputUILayer(List<IEvent> eventQueue)
        {
            UIEvent uiEvent = null;

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                uiEvent = new UIEvent("Left_Mouse_Button_Pressed", currentMouseState.X, currentMouseState.Y);
            }

            else if (currentKeyboardState.IsKeyDown(Keys.I) && previousKeyboardState.IsKeyUp(Keys.I))
            {
                uiEvent = new UIEvent("I_Pressed");
            }

            else if (currentKeyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
            {
                uiEvent = new UIEvent("Escape_Pressed");
            }

            if (uiEvent != null)
            {
                eventQueue.Add(uiEvent);
            }

            return uiEvent;
        }
    }
}
