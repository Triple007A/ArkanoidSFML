using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace BombermanSFML
{
    enum HorizontalDirection
    {
        Left,
        Right
    }

    enum VerticalDirection
    {
        Up,
        Down
    }

    class Ball : IShape, IDrawable
    {
        public Vector2f Position
        {
            get { return Shape.Position; }
            set { Shape.Position = value; }
        }
        public Shape Shape { get; set; }
        public Color Color { get; set; }
        public float Radius { get; set; }

        private float speed = 0;
        public float Speed
        {
            get { return speed; }
            set { if (value < 500) speed = value; }
        }

        HorizontalDirection horizontalDirection = HorizontalDirection.Left;
        VerticalDirection verticalDirection = VerticalDirection.Up;

        public Ball(float radius, Color color)
        {
            Radius = radius;
            Color = color;

            Shape = new CircleShape(Radius);
            Shape.FillColor = Color;

            Speed = 0.1f;
        }

        public void Move(float delta)
        {
            float newX = Position.X;
            float newY = Position.Y;

            if (verticalDirection == VerticalDirection.Up)
                newY -= Speed * delta;
            else
                newY += Speed * delta;

            if (horizontalDirection == HorizontalDirection.Right)
                newX += Speed * delta;
            else
                newX -= Speed * delta;

            Position = new Vector2f(newX, newY);
        }

        public void ChangeVerticalDirection()
        {
            if (verticalDirection == VerticalDirection.Up)
                verticalDirection = VerticalDirection.Down;
            else
                verticalDirection = VerticalDirection.Up;
        }

        public void ChangeHorizontalDirection()
        {
            if (horizontalDirection == HorizontalDirection.Left)
                horizontalDirection = HorizontalDirection.Right;
            else
                horizontalDirection = HorizontalDirection.Left;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(Shape);
        }
    }
}
