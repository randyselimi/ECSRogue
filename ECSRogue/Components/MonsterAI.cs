using System.Collections.Generic;
using ECSRogue.Data;

namespace ECSRogue.Components
{
    public class MonsterAI : Component
    {
        public Stack<int[]> path = new Stack<int[]>();

        public MonsterAI()
        {
        }

        private MonsterAI(MonsterAI monsterAI)
        {
        }

        public override object Clone()
        {
            return new MonsterAI(this);
        }


    }
}