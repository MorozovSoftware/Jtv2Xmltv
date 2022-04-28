using System;
using System.Collections.Generic;
using System.Linq;

namespace Jtv2Xmltv.Core.Extra
{
    internal static class GuideExtension
    {
        /// <summary>
        /// Works only with sorted channel!!! TODO improve it
        /// </summary>
        /// <param name="guide"></param>
        /// <returns></returns>
        public static IGuide SetStopTimeByNext(this IGuide guide)
        {
            foreach (IChannel channel in guide)
            {
                IProg prevProg = null;
                foreach (IProg prog in channel)
                {
                    if (prevProg != null)
                    {
                        prevProg.StopTime = prog.StartTime;
                    }
                    prevProg = prog;
                }
                prevProg.StopTime = DateTime.MaxValue;

            }

            return guide;
        }
    }
}
