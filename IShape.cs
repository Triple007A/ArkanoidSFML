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
    interface IShape
    {
        Vector2f Position { get; set; }

        SFML.Graphics.Shape Shape { get; set; }

        Color Color { get; set; }
    }
}
