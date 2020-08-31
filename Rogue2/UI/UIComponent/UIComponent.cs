using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rogue2.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rogue2.UI
{
    public enum UIPosition
    {
        TopLeft,
        Top,
        TopRight,
        CenterLeft,
        Center,
        CenterRight,
        BottomLeft,
        Bottom,
        BottomRight
    }
    public abstract class UIComponent
    {
        public int ID = 0;

        public Vector2 absolutePosition;
        public Vector2 dimensions;
        public Color color;
        public UIPosition relativePosition { get; set; }
        public Vector2 offset;

        protected List<UIComponent> children = new List<UIComponent>();
        public UIComponent parent { get; set; }

        //used for creating a UI component in which object dimensions are determined post-creation
        public UIComponent(UIPosition relativePosition, Color color, Vector2? offset = null)
        {
            this.relativePosition = relativePosition;
            this.color = color;

            if (offset != null)
            {
                this.offset = (Vector2)offset;
            }

            else { this.offset = Vector2.Zero; }
        }
        public UIComponent(Vector2 dimensions, UIPosition relativePosition, Color color, Vector2? offset = null) : this(relativePosition, color, offset)
        {
            this.dimensions = dimensions;
        }

        virtual public void Draw(Texture2D rectangleSprite, SpriteBatch spriteBatch)
        {

            foreach (var child in children)
            {
                child.Draw(rectangleSprite, spriteBatch);
            }
        }
        virtual public void Update(Vector2 screenOffset)
        {
            absolutePosition.X += (int)screenOffset.X;
            absolutePosition.Y += (int)screenOffset.Y;

            foreach (var child in children)
            {
                child.Update(screenOffset);
            }
           
        }
        virtual public void AddChild(UIComponent childToAdd)
        {
            children.Add(childToAdd);
            childToAdd.parent = this;

            childToAdd.absolutePosition = childToAdd.GetAbsolutePosition(this.absolutePosition, this.dimensions);
            childToAdd.ID = this.children.Count;
        }
        virtual public void RemoveChild(int ID)
        {
            children.RemoveAll(x => x.ID == ID);
        }
        virtual public void RemoveAllChildren()
        {
            foreach (var child in children)
            {
                child.RemoveAllChildren();
            }
            children.Clear();
        }
        virtual public List<T> GetChildren<T>() where T : UIComponent
        {
            List<T> matching = new List<T>();

            if (this.GetType() == typeof(T))
            {
                matching.Add((T)this);
            }

            foreach (var child in children)
            {
                List<T> childMatching = child.GetChildren<T>();

                if (childMatching.Count != 0)
                {
                    matching.AddRange(childMatching);
                }
            }

            return matching;
        }
        public Vector2 GetAbsolutePosition(Vector2 parentPosition, Vector2 parentDimensions)
        {
            Vector2 absolutePosititon = new Vector2();

            if (relativePosition == UIPosition.TopLeft)
            {
                absolutePosititon.X = parentPosition.X;
                absolutePosititon.Y = parentPosition.Y;
            }
            else if (relativePosition == UIPosition.Top)
            {
                absolutePosititon.X = (parentPosition.X + parentDimensions.X / 2) - dimensions.X / 2;
                absolutePosititon.Y = parentPosition.Y;
            }
            else if (relativePosition == UIPosition.TopRight)
            {
                absolutePosititon.X = parentPosition.X + parentDimensions.X - dimensions.X;
                absolutePosititon.Y = parentPosition.Y;
            }

            else if (relativePosition == UIPosition.CenterLeft)
            {
                absolutePosititon.X = parentPosition.X;
                absolutePosititon.Y = (parentPosition.Y + parentDimensions.Y / 2) - dimensions.Y / 2;
            }
            else if (relativePosition == UIPosition.Center)
            {
                absolutePosititon.X = (parentPosition.X + parentDimensions.X / 2) - dimensions.X / 2;
                absolutePosititon.Y = (parentPosition.Y + parentDimensions.Y / 2) - dimensions.Y / 2;
            }
            else if (relativePosition == UIPosition.CenterRight)
            {
                absolutePosititon.X = parentPosition.X + parentDimensions.X - dimensions.X;
                absolutePosititon.Y = (parentPosition.Y + parentDimensions.Y / 2) - dimensions.Y / 2;
            }

            else if (relativePosition == UIPosition.BottomLeft)
            {
                absolutePosititon.X = parentPosition.X;
                absolutePosititon.Y = parentPosition.Y + parentDimensions.Y - dimensions.Y;
            }
            else if (relativePosition == UIPosition.Bottom)
            {
                absolutePosititon.X = (parentPosition.X + parentDimensions.X / 2) - dimensions.X / 2;
                absolutePosititon.Y = parentPosition.Y + parentDimensions.Y - dimensions.Y;
            }
            else if (relativePosition == UIPosition.BottomRight)
            {
                absolutePosititon.X = parentPosition.X + parentDimensions.X - dimensions.X;
                absolutePosititon.Y = parentPosition.Y + parentDimensions.Y - dimensions.Y;
            }

            return absolutePosititon;
        }
    }
}
