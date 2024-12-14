using DinkToPdf;
using DinkToPdf.Contracts;
using VehicleShowroom.Management.Infrastructure.Abstracts;

namespace VehicleShowroom.Management.Infrastructure.Services
{
    public class PDFService : IPDFService
    {
        private readonly IConverter _conventor;

        public PDFService(IConverter conventor)
        {
            _conventor = conventor;
        }
        public byte[] GeneratePDF(string contentHTML,
                                     Orientation orientation = Orientation.Portrait,
                                     PaperKind paperKind = PaperKind.A4)
        {
            var globalSetting = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = orientation,
                PaperSize = paperKind,
                Margins = new MarginSettings() { Top = 10, Bottom = 10 },
            };

            var objectSettings = new ObjectSettings()
            {
                PagesCount = true,
                HtmlContent = contentHTML,
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = new HeaderSettings
                {
                   /* FontSize = 12,
                    Right = "Page [page] of [toPage]",
                    Line = true,
                    Spacing = 5.812*/
                },
                FooterSettings = new FooterSettings
                {
                   /* FontSize = 10,
                    Center = "Footer content here",
                    Line = true,
                    Spacing = 2.812*/
                }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSetting,
                Objects = { objectSettings }
            };

            return _conventor.Convert(pdf);
        }
    }
}
