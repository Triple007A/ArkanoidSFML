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
    static class Score
    {
        static Font font = new Font("arial.ttf");
        static Text scoreText = new Text();

        static int scoreValue;

        public static Vector2f Position
        {
            get { return scoreText.Position; }
            set { scoreText.Position = value; }
        }


        static Score()
        {
            scoreValue = 0;

            scoreText.Font = font;
            scoreText.DisplayedString = "Score: " + scoreValue;
            scoreText.CharacterSize = 24;
            scoreText.Color = Color.Cyan;
            scoreText.Style = Text.Styles.Bold;

            scoreText.Position = new Vector2f(20.0f, 20.0f);
        }

        public static void UpdateScore(int pointsToadd)
        {
            scoreValue += pointsToadd;

            scoreText.DisplayedString = "Score: " + scoreValue;
        }

        public static void Draw(RenderWindow window)
        {
            window.Draw(scoreText);
        }
    }
}
