using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Linq;
using UsefulTools;

namespace Jtv2Xmltv.Core
{
    public class JtvGuide
    {
        readonly Dictionary<string, JtvChannel> channels = new();

        public void OpenJtv(string filePath, Encoding zipEncoding, Encoding pdtEncoding)
        {
            using (ZipArchive zipArchive = ZipFile.Open(filePath, ZipArchiveMode.Read, zipEncoding))
            {
                foreach (ZipArchiveEntry entry in zipArchive.Entries)
                {
                    switch(Path.GetExtension(entry.Name))
                    {
                        case ".ndx":
                            using (var file = entry.Open())
                            {
                                DictionaryTools.AddOrGet(Path.GetFileNameWithoutExtension(entry.Name), channels).ReadNDX(file);
                            }
                            break;
                        case ".pdt":
                            using (var file = entry.Open())
                            {
                                DictionaryTools.AddOrGet(Path.GetFileNameWithoutExtension(entry.Name), channels).ReadPDT(file, pdtEncoding);
                            }
                            break;
                    }
                }
            }
        }
        
        public void CreateXmltv(string languageAttribute, string timeOffsetAttribute, Dictionary<string, string> renameChannelsMap)
        {
            XDocument xmltv = new();
            XElement tvXml = new("tv");
            List<XElement> programsXml = new(channels.Count);

            int channelId = 1;

            foreach (KeyValuePair<string, JtvChannel> channel in channels)
            {
                tvXml.Add(new XElement("channel",
                    new XAttribute("id", channelId),
                    new XElement("display-name",
                        new XAttribute("lang", languageAttribute),
                        DictionaryTools.GetReplacedOrDefault(channel.Key, renameChannelsMap))));

                programsXml.AddRange(channel.Value.GetProgramsXML(channelId, languageAttribute, timeOffsetAttribute));

                channelId++;
            }

            tvXml.Add(programsXml);

            xmltv.Add(tvXml);
            xmltv.Save("xmltv.xml");
        }

        public void CreateXmltv(string languageAttribute, string timeOffsetAttribute)
        {
            CreateXmltv(languageAttribute, timeOffsetAttribute, new Dictionary<string, string>());
        }

    }
}
