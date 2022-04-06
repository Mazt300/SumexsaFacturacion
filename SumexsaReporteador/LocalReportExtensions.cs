using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;

namespace Microsoft.Reporting.WinForms
{
    public static class LocalReportExtensions
    {
        public static void PrintToPrinter(this LocalReport report)
        {
            PageSettings pageSettings = new PageSettings();
            pageSettings.PaperSize = report.GetDefaultPageSettings().PaperSize;
            pageSettings.Margins = report.GetDefaultPageSettings().Margins;
            pageSettings.Landscape = report.GetDefaultPageSettings().IsLandscape;
            Print(report,pageSettings);
        }

        public static void Print(LocalReport report, PageSettings pageSettings)
        {
            string deviceinfo =
                 $@"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>{pageSettings.PaperSize.Width}</PageWidth>
                <PageHeight>{pageSettings.PaperSize.Height}</PageHeight>
                <MarginTop>{pageSettings.Margins.Top}</MarginTop>
                <MarginLeft>{pageSettings.Margins.Left}</MarginLeft>
                <MarginRight>{pageSettings.Margins.Right}</MarginRight>
                <MarginBottom>{pageSettings.Margins.Bottom}</MarginBottom>
            </DeviceInfo>";
            ReportParameterInfoCollection reportParameter;
            reportParameter = report.GetParameters();
            Warning[] warnings;
            var streams = new List<Stream>();
            var pageIndex = 0;
            report.Render("Image",deviceinfo,(name,filenameExtension,encoding,mimetype,willSeek) =>
            {
                MemoryStream stream = new MemoryStream();
                streams.Add(stream);
                return stream;
            }, out warnings);
            foreach (Stream stream in streams)
            {
                stream.Position = 0;
                if (stream == null || streams.Count == 0)
                {
                    throw new Exception("No stream to print.");
                }
            }
            using (PrintDocument printDocument = new PrintDocument())
            {
                printDocument.DefaultPageSettings = pageSettings;
                if (!printDocument.PrinterSettings.IsValid)
                {
                    throw new Exception("Can't fomd the default printer.");
                }
                else
                {
                    printDocument.PrintPage += (sender, e) =>
                    {
                        Metafile pageimage = new Metafile(streams[pageIndex]);
                        Rectangle adjustedRect = new Rectangle(e.PageBounds.Left - (int)e.PageSettings.HardMarginX,
                            e.PageBounds.Top - (int)e.PageSettings.HardMarginY,
                            e.PageBounds.Width,
                            e.PageBounds.Height);
                        e.Graphics.FillRectangle(Brushes.White, adjustedRect);
                        e.Graphics.DrawImage(pageimage, adjustedRect);
                        pageIndex++;
                        e.HasMorePages = (pageIndex < streams.Count);
                        //e.Graphics.DrawRectangle(Pens.Red, adjustedRect);
                    };
                    printDocument.EndPrint += (sender, e) =>
                    {
                        if (streams != null)
                        {
                            foreach (Stream stream in streams)
                            {
                                stream.Close();
                                streams = null;
                            }
                        }
                    };
                    printDocument.Print();
                }
            }
        }
    }
}
