using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace PdfConsoleTest.PdfHelpers
{
    public class HeaderFooter : PdfPageEventHelper
    {
        private ElementList HeaderElements { get; set; }
        private ElementList FooterElements { get; set; }

        public HeaderFooter(ElementList headerElements, ElementList footerElements)
        {
            HeaderElements = headerElements;
            FooterElements = footerElements;
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            try
            {
                ColumnText headerText = new ColumnText(writer.DirectContent);
                foreach (IElement e in HeaderElements)
                {
                    headerText.AddElement(e);
                }
                headerText.SetSimpleColumn(document.Left, document.Top, document.Right, document.GetTop(-100), 10, Element.ALIGN_MIDDLE);
                headerText.Go();

                ColumnText footerText = new ColumnText(writer.DirectContent);
                foreach (IElement e in FooterElements)
                {
                    footerText.AddElement(e);
                }
                footerText.SetSimpleColumn(document.Left, document.GetBottom(-100), document.Right, document.GetBottom(-40), 10, Element.ALIGN_MIDDLE);
                footerText.Go();
            }
            catch (DocumentException de)
            {
                throw new Exception(de.Message);
            }
        }
    }
}