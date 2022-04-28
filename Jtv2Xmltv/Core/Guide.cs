using System.Collections;
using System.Collections.Generic;

namespace Jtv2Xmltv.Core
{
    internal class Guide : IGuide
    {
        private readonly List<IChannel> channels = new();

        public void AddChannel(IChannel channel)
        {
            channels.Add(channel);
        }

        public IEnumerator<IChannel> GetEnumerator()
        {
            return channels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return channels.GetEnumerator();
        }
    }
}
