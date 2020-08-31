using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    public class MonsterAI : Component
    {
        public Stack<Vector2> path = new Stack<Vector2>();

        public MonsterAI()
        {
            
        }

        MonsterAI(MonsterAI monsterAI)
        {

        }

        public override object Clone()
        {
            return new MonsterAI(this);
        }
    }
}
