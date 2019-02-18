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

        public bool IsActive { get; set; }

        public Brick(Vector2f position, Color color, Vector2f size)
        {
            Size = size;
            Color = color;

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
                    ((Ball)other).ChangeVerticalDirection();
                    IsActive = false;
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
