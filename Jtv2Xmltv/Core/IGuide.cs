using System.Collections.Generic;

namespace Jtv2Xmltv.Core
{
    internal interface IGuide : IEnumerable<IChannel>
    {
        void AddChannel(IChannel channel);
    }
}
