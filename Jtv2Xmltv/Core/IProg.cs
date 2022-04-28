using System;

namespace Jtv2Xmltv.Core
{
    internal interface IProg
    {
        DateTime StartTime { get; set; }
        DateTime StopTime { get; set; }
        string Name { get; set; }

    }
}
