using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidSFML
{
    interface ICollidable
    {
        void CheckForCollision(object other);
    }
}
