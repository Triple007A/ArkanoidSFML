using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace BombermanSFML
{
    public enum BoundType
    {
        Left,
        Right,
        Upper,
        Bottom
    }

    class Bound : IShape, ICollidable, IDrawable
    {
        public Vector2f Position
        {
            get { return Shape.Position; }
            set { Shape.Position = value; }
        }

        public Shape Shape { get; set; }
        public Color Color { get; set; }
        public Vector2f Size { get; set; }

        public BoundType Type { get; set; }

        public Bound(Vector2f position, Vector2f size, Color color, BoundType type)
        {
            Type = Type;
            Size = size;
            Color = color;

            Shape = new RectangleShape(Size);
            Position = position;

            Shape.FillColor = Color;
        }

        public void CheckForCollision(object other)
        {
            if (Shape.GetGlobalBounds().Intersects(((Ball)other).Shape.GetGlobalBounds()))
            {
                if (other is Ball)
                {
                    if (Type == BoundType.Left || Type == BoundType.Right)
                        ((Ball)other).ChangeHorizontalDirection();
                    else if(Type == BoundType.Upper || Type == BoundType.Bottom)
                        ((Ball)other).ChangeVerticalDirection();
                }
            }
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(Shape);
        }
    }
}
