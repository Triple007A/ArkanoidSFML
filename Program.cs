using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;

namespace ArkanoidSFML
{
    class Program
    {
        static RenderWindow window;
        static readonly uint screenWidth = 1000;
        static readonly uint screenHeight = 800;

        static Bound leftBound = new Bound(new Vector2f(0.0f, 0.0f), new Vector2f(20.0f, screenHeight), Color.White, BoundType.Left);
        static Bound rightBound = new Bound(new Vector2f((float)screenWidth - 20.0f, 0.0f), new Vector2f(20.0f, screenHeight), Color.White, BoundType.Right);

        static Bound upperBound = new Bound(new Vector2f(0.0f, 0.0f), new Vector2f(screenWidth, 20.0f), Color.White, BoundType.Upper);

        static Player player = new Player();
        static List<Brick> brics = new List<Brick>();
        static Ball ball = new Ball(12, Color.White);

        static Clock clock = new Clock();
        static Time oldTime = new Time();
        static Time newTime = new Time();

        static void Main(string[] args)
        {
            window = new RenderWindow(new VideoMode(screenWidth, screenHeight), "Arkanoid", Styles.Default);
            Init();

            clock.Restart();

            while (window.IsOpen)
            {
                if (!Update())
                    break;

                UpdatePhysics();

                Draw();

                window.Display();
            }
        }

        static bool CheckGameOver(Ball ball)
        {
            if (ball.Position.Y > screenWidth)
                return true;

            return false;
        }

        static void Init()
        {
            window.SetFramerateLimit(60);
            window.Closed += (sender, arg) => window.Close();
            window.MouseMoved += player.PlayerMove;

            for (int i = 1; i < 8; i++)
            {
                brics.Add(new Brick(new Vector2f(i * 120.0f, 50.0f), Color.Red, new Vector2f(70.0f, 20.0f)));
                brics.Add(new Brick(new Vector2f(i * 120.0f, 120.0f), Color.Green, new Vector2f(70.0f, 20.0f)));
                brics.Add(new Brick(new Vector2f(i * 120.0f, 190.0f), Color.Yellow, new Vector2f(70.0f, 20.0f)));
                brics.Add(new Brick(new Vector2f(i * 120.0f, 260.0f), Color.Magenta, new Vector2f(70.0f, 20.0f)));
            }

            player.Position = new Vector2f(500, 700);

            ball.Position = new Vector2f(530, 500);
            ball.Speed = 300;
        }

        static bool Update()
        {
            window.DispatchEvents();
            window.Clear(Color.Black);

            oldTime = newTime;
            newTime = clock.ElapsedTime;
            float delta = (newTime - oldTime).AsSeconds();

            ball.Move(delta);

            if (CheckGameOver(ball))
                return false;

            return true;
        }

        static void UpdatePhysics()
        {
            for (int i = 0; i < brics.Count; i++)
                brics[i].CheckForCollision(ball);

            player.CheckForCollision(ball);
            leftBound.CheckForCollision(ball);
            rightBound.CheckForCollision(ball);
            upperBound.CheckForCollision(ball);
        }

        static void DrawScreenBounds(RenderWindow window)
        {
            leftBound.Draw(window);
            rightBound.Draw(window);
            upperBound.Draw(window);
        }

        static void Draw()
        {
            Score.Draw(window);

            player.Draw(window);
            ball.Draw(window);
            DrawScreenBounds(window);

            for (int i = 0; i < brics.Count; i++)
                brics[i].Draw(window);
        }
    }
}
