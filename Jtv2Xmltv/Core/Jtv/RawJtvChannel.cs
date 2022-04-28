using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using UsefulTools;

namespace Jtv2Xmltv.Core.Jtv
{
    internal class RawJtvChannel
    {
        public readonly Dictionary<ulong, int> programs = new();
        public readonly Dictionary<int, string> programNames = new();
        
        internal void ReadNDX(Stream stream)
        {
            byte[] countOfRecordsBytes = new byte[2];
            stream.Read(countOfRecordsBytes, 0, countOfRecordsBytes.Length);

            ushort countOfRecords = ForcedBitConverter.GetUshortLittleEndian(countOfRecordsBytes, 0);

            byte[] record = new byte[12];
            for (ushort i = 0; i < countOfRecords; i++)
            {                
                stream.Read(record, 0, record.Length);
                programs.Add(ForcedBitConverter.GetUint64LittleEndian(record, 2), ForcedBitConverter.GetUshortLittleEndian(record, 10));
            }
        }

        internal void ReadPDT(Stream stream, Encoding encoding)
        {
            byte[] formatHeader = new byte[26];
            stream.Read(formatHeader, 0, formatHeader.Length);

            int RecordOffset = formatHeader.Length;

            byte[] countOfCharsBytes = new byte[2];
            while (stream.Read(countOfCharsBytes, 0, countOfCharsBytes.Length) == 2)
            {
                int countOfChars = ForcedBitConverter.GetUshortLittleEndian(countOfCharsBytes, 0);
                byte[] nameBytes = new byte[countOfChars];
                stream.Read(nameBytes, 0, nameBytes.Length);

                programNames.Add(RecordOffset, encoding.GetString(nameBytes));

                RecordOffset += countOfCharsBytes.Length + nameBytes.Length;
            }
        }
        /// <summary>
        /// Deprecated
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="languageAttribute"></param>
        /// <param name="timeOffsetAttribute"></param>
        /// <returns></returns>
        internal List<XElement> GetProgramsXML(int channelId, string languageAttribute, string timeOffsetAttribute)
        {
            List<XElement> programsXML = new(programs.Count);
            XElement programme = null;

            foreach (KeyValuePair<ulong, int> program in programs)
            {
                var currentStartTime = $"{DateTime.FromFileTimeUtc((long)program.Key):yyyyMMddHHmmss} {timeOffsetAttribute}";

                programme?.Attribute("stop").SetValue(currentStartTime);

                programme = new XElement("programme",
                    new XAttribute("start", currentStartTime),
                    new XAttribute("stop", currentStartTime),
                    new XAttribute("channel", channelId),
                    new XElement("title",
                        new XAttribute("lang", languageAttribute),
                        programNames[program.Value]));

                programsXML.Add(programme);
            }

            return programsXML;
        }
    }
}
