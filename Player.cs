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

        private float physicsScale;

        public Player()
        {
            Size = new Vector2f(100.0f, 20.0f);
            Color = Color.Blue;

            physicsScale = Size.Y / Size.X;

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
                    var delta = ((Ball)other).Position - this.Shape.Position;
                    Vector2f scaled = new Vector2f(delta.X * physicsScale, delta.Y * 1.0f);
                    if (Math.Abs(scaled.X) >= Math.Abs(scaled.Y))
                        ((Ball)other).ChangeHorizontalDirection();
                    else
                        ((Ball)other).ChangeVerticalDirection();

                    /*float enlargeScale = physicsScale;

                    //float angle = -((((Ball)other).Position.X - this.Position.X)) + 90.0f;
                    float angle = (-(((Ball)other).Position.X - this.Position.X) * 60f / enlargeScale) + 90f;

                    double velolotyY = Math.Tan(angle);
                    double velocityX = 1 / Math.Tan(angle);

                    //((Ball)other).Velocity = new Vector2f((float)velocityX, (float)velolotyY);
                    Console.WriteLine(angle);*/
                }
            }
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(Shape);
        }
    }
}
