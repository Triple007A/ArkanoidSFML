using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace BombermanSFML
{
    class Brick : IShape, ICollidable, IDrawable
    {
        public Vector2f Position
        {
            get { return Shape.Position; }
            set { Shape.Position = value; }
        }

        public Vector2f Size { get; set; }
        public Color Color { get; set; }
        public Shape Shape { get; set; }

        private float physicsScale;

        public bool IsActive { get; set; }

        public Brick(Vector2f position, Color color, Vector2f size)
        {
            Size = size;
            Color = color;

            physicsScale = Size.Y / Size.X;

            Shape = new RectangleShape(Size);

            Shape.FillColor = color;
            Position = position;

            IsActive = true;
        }

        public void CheckForCollision(object other)
        {
            if (IsActive && Shape.GetGlobalBounds().Intersects(((Ball)other).Shape.GetGlobalBounds()))
            {
                if (other is Ball)
                {
                    var delta = ((Ball)other).Position - this.Shape.Position;
                    Vector2f scaled = new Vector2f(delta.X * physicsScale, delta.Y);
                    if (Math.Abs(scaled.X) >= Math.Abs(scaled.Y))
                    {
                        ((Ball)other).ChangeHorizontalDirection();
                        Score.UpdateScore(1);
                        IsActive = false;
                    }
                    else
                    {
                        ((Ball)other).ChangeVerticalDirection();
                        Score.UpdateScore(1);
                        IsActive = false;
                    }
                }
            }
        }

        public void Draw(RenderWindow window)
        {
            if(IsActive)
                window.Draw(Shape);
        }
    }
}
