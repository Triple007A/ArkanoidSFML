﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace ArkanoidSFML
{
    interface IDrawable
    {
        void Draw(RenderWindow window);
    }
}
