using Jtv2Xmltv.Core;
using System;
using System.Text;

namespace Jtv2Xmltv
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var jtvPath = args.Length!=0?args[0]:Console.ReadLine();
            var zipEncoding = Encoding.GetEncoding(866);
            var pdtEncoding = Encoding.GetEncoding(1251);
            var language = "ru";
            var timeOffset = "+0300";

            JtvGuide guide = new();
            guide.OpenJtv(jtvPath, zipEncoding, pdtEncoding);
            guide.CreateXmltv(language, timeOffset);
        }
    }
}
