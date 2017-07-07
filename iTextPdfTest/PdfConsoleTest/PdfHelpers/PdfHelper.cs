using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace PdfConsoleTest.PdfHelpers
{
    public static class PdfHelper
    {

        private static string headerHtml =
            "<table width=\"100%\" border=\"0\"><tr><td>Header</td><td align=\"right\">Some title</td></tr></table>";

        private static string footerHtml =
            "<table width=\"100%\" border=\"0\"><tr><td>Footer</td><td align=\"right\">Some title</td></tr></table>";
        
        public static byte[] CreatePdf()
        {
            byte[] bytes;

            var document = new Document(PageSize.A4, 40, 40, 120, 120);

            // Create a new PdfWriter object, specifying the output stream
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            // Password Setup
            // writer.SetEncryption(false, "Password", "Password12!@", 2);

            // Open the Document for writing
            document.Open();

            var headerElements = new HtmlElementHandler();
            var footerElements = new HtmlElementHandler();

            XMLWorkerHelper.GetInstance().ParseXHtml(headerElements, new StringReader(headerHtml));
            XMLWorkerHelper.GetInstance().ParseXHtml(footerElements, new StringReader(footerHtml));

            writer.PageEvent = new HeaderFooter(headerElements.GetElements(), footerElements.GetElements());

            //var html = RenderRazorViewToString("PdfView", pdfViewModel);
            var html = File.ReadAllText("testhtml.html");

            var worker = new HTMLWorker(document);
            
            // TODO: Load CSS
            //var css = new StyleSheet();
            //css.LoadTagStyle(HtmlTags.TH, HtmlTags.BGCOLOR, "#616161");
            //css.LoadTagStyle(HtmlTags.TH, HtmlTags.COLOR, "#fff");
            //css.LoadTagStyle(HtmlTags.BODY, HtmlTags.FONT, "verdana");
            //css.LoadTagStyle(HtmlTags.TH, HtmlTags.FONTWEIGHT, "bold");
            //css.LoadStyle("even", "bgcolor", "#EEE");

            //worker.SetStyleSheet(css);
            var stringReader = new StringReader(html);
            // worker.Parse(stringReader);
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, stringReader);
            document.Close();

            return output.ToArray();
        }

        private static string RenderRazorViewToString(string viewName, object model)
        {
            //ViewData.Model = model;
            //using (var sw = new StringWriter())
            //{
            //    var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
            //    var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
            //    viewResult.View.Render(viewContext, sw);
            //    viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
            //    return sw.GetStringBuilder().ToString();
            //}

            return string.Empty;
        }
    }
}
