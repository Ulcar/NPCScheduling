using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
   
    // still need to figure this out properly
    public class GameTime
    {


        private float internalTimeValue;


        // Game time Added Per second
        public int ScaleValue;



        public void UpdateTime(float deltaTime) 
        {
            internalTimeValue += deltaTime * ScaleValue;
        }

    }
}
