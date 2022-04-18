using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Challenges.C_Gangs
{
    public abstract class GangChallenge
    {
        public abstract string leaderAgent { get; }
        public abstract string[] middleAgents { get; } // Can be in order, or a random pool?
        public abstract string lastAgent { get; }
    }
}
