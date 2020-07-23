using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons
{
    public class PlayerBehaviour : Behaviour
    {
        public override void Run(ref Entity caller, ref Level level, GameTime gameTime)
        {
            if(!(caller.HasAttribute("Position") && caller.HasAttribute("Direction")))
            {
                return;
            }

            if (InputState.HasAction("W"))
            {

            }
            if (InputState.HasAction("S"))
            {
                camera.Move(gameTime, false);
            }
            if (InputState.HasAction("A"))
            {
                camera.Rotate(90);
            }
            if (InputState.HasAction("D"))
            {
                camera.Rotate(-90);
            }
        }



    }
}
