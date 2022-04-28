using System.Collections;
using System.Collections.Generic;

namespace Jtv2Xmltv.Core
{
    internal class Channel : IChannel
    {
        private readonly List<IProg> programs = new();

        public string Name { get; set; }

        public void AddProg(IProg prog)
        {
            programs.Add(prog);
        }

        public IEnumerator<IProg> GetEnumerator()
        {
            return programs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return programs.GetEnumerator();
        }
    }
}
