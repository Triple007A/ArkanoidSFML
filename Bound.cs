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

        private float physicsScale;

        public BoundType Type { get; set; }

        public Bound(Vector2f position, Vector2f size, Color color, BoundType type)
        {
            Type = Type;
            Size = size;
            Color = color;

            physicsScale = Size.X / Size.Y; ;

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
                    Vector2f scaled;
                    var delta = ((Ball)other).Position - this.Shape.Position;

                    if (Type == BoundType.Bottom || Type == BoundType.Upper)
                        scaled = new Vector2f(delta.X * physicsScale, delta.Y * 1.0f);
                    else
                        scaled = new Vector2f(delta.X * 1.0f, delta.Y * physicsScale);

                    if (Math.Abs(scaled.X) >= Math.Abs(scaled.Y))
                        ((Ball)other).ChangeHorizontalDirection();
                    else
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
