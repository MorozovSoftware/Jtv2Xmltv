using Jtv2Xmltv.Core.Jtv;
using Jtv2Xmltv.Core.Xmltv;
using Jtv2Xmltv.Core.Extra;
using System;

namespace Jtv2Xmltv
{
    class Program
    {
        static void Main(string[] args)
        {
            var jtvPath = args.Length!=0?args[0]:Console.ReadLine();
            
            JtvCoder jtv = new();
            XmltvCoder xmltv = new();

            xmltv.Save(jtv.Open(jtvPath).SetStopTimeByNext());



        }
    }
}
