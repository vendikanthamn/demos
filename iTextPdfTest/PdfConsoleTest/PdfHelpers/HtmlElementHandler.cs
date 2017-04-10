using iTextSharp.text;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline;

namespace PdfConsoleTest.PdfHelpers
{
    public class HtmlElementHandler : IElementHandler
    {
        public ElementList Elements { get; set; }

        public HtmlElementHandler()
        {
            Elements = new ElementList();
        }

        public ElementList GetElements()
        {
            return Elements;
        }

        public void Add(IWritable w)
        {
            if (w is WritableElement)
            {
                foreach (IElement e in ((WritableElement)w).Elements())
                {
                    Elements.Add(e);
                }
            }
        }
    }
}