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
    class Player : IShape, ICollidable, IDrawable
    {
        public Vector2f Position
        {
            get { return Shape.Position; }
            set { Shape.Position = value; }
        }

        public Vector2f Size { get; set; }
        public Color Color { get; set; }
        public Shape Shape { get; set; }

        public Player()
        {
            Size = new Vector2f(100.0f, 20.0f);
            Color = Color.Blue;

            Shape = new RectangleShape(Size);
            Shape.FillColor = Color;

            Position = new Vector2f(0, 0);

        }

        public void PlayerMove(object sender, MouseMoveEventArgs e)
        {
            Position = new Vector2f(e.X, Position.Y);
        }

        public void CheckForCollision(object other)
        {
            if (Shape.GetGlobalBounds().Intersects(((Ball)other).Shape.GetGlobalBounds()))
            {
                if(other is Ball)
                {
                    ((Ball)other).ChangeVerticalDirection();
                    ((Ball)other).Speed += 50;
                }
            }
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(Shape);
        }
    }
}
