using System.Text;

namespace Jtv2Xmltv.Core.Jtv
{
    internal class JtvCoder : IOpenGuide
    {
        Encoding zipEncoding;
        Encoding pdtEncoding;
        public void SetZipEncoding(Encoding encoding)
        {
            zipEncoding = encoding;
        }
        public void SetPdtEncoding(Encoding encoding)
        {
            pdtEncoding = encoding;
        }



        public JtvCoder()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            zipEncoding = Encoding.GetEncoding(866);
            pdtEncoding = Encoding.GetEncoding(1251);
        }



        public IGuide Open(string path)
        {
            RawJtvGuide jtvGuide = new();
            jtvGuide.Open(path, zipEncoding, pdtEncoding);
            
            return jtvGuide.CreateGuide();
        }
    }
}
