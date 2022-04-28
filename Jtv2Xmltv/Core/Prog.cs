using System;

namespace Jtv2Xmltv.Core
{
    internal class Prog : IProg
    {
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public string Name { get; set; }
    }
}
