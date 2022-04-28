using System.Collections.Generic;
using System.Xml.Linq;

namespace Jtv2Xmltv.Core.Xmltv
{
    internal class XmltvCoder : ISaveGuide
    {
        string language = "ru";    
        string timeOffset = "+0300";

        public void SetLanguage(string language)
        {
            this.language = language;
        }
        public void SetTimeOffset(string timeOffset)
        {
            this.timeOffset = timeOffset;
        }

        public void Save(IGuide guide)
        {
            XDocument xmltv = new();
            XElement tvXml = new("tv");
            List<XElement> programsXml = new();

            int channelId = 1;

            foreach (IChannel channel in guide)
            {
                
                tvXml.Add(new XElement("channel",
                    new XAttribute("id", channelId),
                    new XElement("display-name",
                        new XAttribute("lang", language),
                        channel.Name)));

                foreach (IProg prog in channel)
                {
                    programsXml.Add(new XElement("programme",
                            new XAttribute("start", $"{prog.StartTime:yyyyMMddHHmmss} {timeOffset}"),
                            new XAttribute("stop", $"{prog.StopTime:yyyyMMddHHmmss} {timeOffset}"),
                            new XAttribute("channel", channelId),
                            new XElement("title",
                                new XAttribute("lang", language),
                                prog.Name)));
                }
                channelId++;
            }

            tvXml.Add(programsXml);

            xmltv.Add(tvXml);
            xmltv.Save("xmltv.xml");
        }
    }
}
