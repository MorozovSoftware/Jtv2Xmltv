using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Linq;
using UsefulTools;

namespace Jtv2Xmltv.Core.Jtv
{
    internal class RawJtvGuide
    {
        readonly Dictionary<string, RawJtvChannel> channels = new();

        internal void Open(string filePath, Encoding zipEncoding, Encoding pdtEncoding)
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
        public IGuide CreateGuide()
        {
            IGuide guide = new Guide();

            foreach (KeyValuePair<string, RawJtvChannel> jtvChannel in channels)
            {
                IChannel channel = new Channel
                {
                    Name = jtvChannel.Key
                };

                foreach (KeyValuePair<ulong, int> program in jtvChannel.Value.programs)
                {
                    IProg prog = new Prog
                    {
                        StartTime = DateTime.FromFileTimeUtc((long)program.Key),
                        Name = jtvChannel.Value.programNames[program.Value]
                    };
                    channel.AddProg(prog);
                }

                guide.AddChannel(channel);
            }
            
            return guide;
        }

        /// <summary>
        /// Deprecated
        /// </summary>
        /// <param name="languageAttribute"></param>
        /// <param name="timeOffsetAttribute"></param>
        /// <param name="renameChannelsMap"></param>
        internal void CreateXmltv(string languageAttribute, string timeOffsetAttribute, Dictionary<string, string> renameChannelsMap)
        {
            XDocument xmltv = new();
            XElement tvXml = new("tv");
            List<XElement> programsXml = new(channels.Count);

            int channelId = 1;

            foreach (KeyValuePair<string, RawJtvChannel> channel in channels)
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
        /// <summary>
        /// Deprecated
        /// </summary>
        /// <param name="languageAttribute"></param>
        /// <param name="timeOffsetAttribute"></param>
        internal void CreateXmltv(string languageAttribute, string timeOffsetAttribute)
        {
            CreateXmltv(languageAttribute, timeOffsetAttribute, new Dictionary<string, string>());
        }

    }
}
