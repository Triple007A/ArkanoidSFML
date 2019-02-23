using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;

namespace BombermanSFML
{
    class Program
    {
        static uint screenWidth = 1000;
        static uint screenHeight = 800;

        static Bound leftBound = new Bound(new Vector2f(0.0f, 0.0f), new Vector2f(20.0f, screenHeight), Color.White, BoundType.Left);
        static Bound rightBound = new Bound(new Vector2f((float)screenWidth - 20.0f, 0.0f), new Vector2f(20.0f, screenHeight), Color.White, BoundType.Right);

        static Bound upperBound = new Bound(new Vector2f(0.0f, 0.0f), new Vector2f(screenWidth, 20.0f), Color.White, BoundType.Upper);
        //static Bound bottomBound = new Bound(new Vector2f(0.0f, (float)screenHeight - 20.0f), new Vector2f(screenWidth, 20.0f), Color.White, BoundType.Bottom);

        static Player player = new Player();
        static List<Brick> brics = new List<Brick>();
        static Ball ball = new Ball(10, Color.Green);
        
        static void Main(string[] args)
        {
            var window = new RenderWindow(new VideoMode(screenWidth, screenHeight), "Bomberman", Styles.Default);
            window.SetFramerateLimit(60);
            window.Closed += (sender, arg) => window.Close();
            window.MouseMoved += player.PlayerMove;

            for (int i = 0; i < 10; i++)
            {
                brics.Add(new Brick(new Vector2f(i * 100.0f, 50.0f), Color.Red, new Vector2f(70.0f, 20.0f)));
                brics.Add(new Brick(new Vector2f(i * 100.0f, 100.0f), Color.Green, new Vector2f(70.0f, 20.0f)));
            }

            player.Position = new Vector2f(500, 700);

            ball.Position = new Vector2f(530, 500);
            ball.Speed = 300;

            Clock clock = new Clock();
            clock.Restart();

            Time oldTime = new Time();
            Time newTime = new Time();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.Black);

                oldTime = newTime;
                newTime = clock.ElapsedTime;
                float delta = (newTime - oldTime).AsSeconds();

                ball.Move(delta);

                if (CheckGameOver(ball))
                    break;

                Score.Draw(window);

                player.Draw(window);
                ball.Draw(window);
                DrawScreenBounds(window);

                for (int i = 0; i < brics.Count; i++)
                {
                    brics[i].Draw(window);
                    brics[i].CheckForCollision(ball);
                }

                player.CheckForCollision(ball);
                leftBound.CheckForCollision(ball);
                rightBound.CheckForCollision(ball);
                upperBound.CheckForCollision(ball);
                //bottomBound.CheckForCollision(ball);

                window.Display();
            }
        }

        static void DrawScreenBounds(RenderWindow window)
        {
            leftBound.Draw(window);
            rightBound.Draw(window);
            upperBound.Draw(window);
           // bottomBound.Draw(window);
        }

        static bool CheckGameOver(Ball ball)
        {
            if (ball.Position.Y > screenWidth)
                return true;

            return false;
        }
    }
}
