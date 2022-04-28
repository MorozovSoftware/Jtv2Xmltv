using System.Collections.Generic;

namespace Jtv2Xmltv.Core
{
    internal interface IChannel : IEnumerable<IProg>
    {
        string Name { get; set; }
        void AddProg(IProg prog);
    }
}
