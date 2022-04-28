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
    }
}
