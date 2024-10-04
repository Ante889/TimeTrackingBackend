using PdfSharp.Drawing;
using PdfSharp.Pdf;
using MediatR;
using TimeTracking.App.Phase.Application.Query;
using TimeTracking.App.Category.Application.Query;
using TimeTracking.App.Time.Application.Query;
using TimeTracking.App.Phase.Domain.Entity;
using TimeTracking.App.Category.Domain.Entity;
using TimeTracking.App.Time.Domain.Entity;

namespace TimeTracking.App.Phase.Infrastructure.Service
{
    public class PhaseCalculationService
    {
        private readonly IMediator _mediator;

        public PhaseCalculationService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<byte[]> GeneratePhasePdf(int phaseId)
        {
            var phase = await _mediator.Send(new FindPhaseByIdQuery(phaseId));
            if (phase == null) throw new KeyNotFoundException($"Phase with ID {phaseId} not found.");

            var categories = await _mediator.Send(new FindCategoriesByPhaseQuery(phase));

            using var stream = new MemoryStream();
            var document = await CreatePdfDocument(phase, categories, stream);
            document.Save(stream, false);
            stream.Position = 0;
            return stream.ToArray();
        }

        private async Task<PdfDocument> CreatePdfDocument(PhaseEntity phase, IEnumerable<CategoryEntity> categories, MemoryStream stream)
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var fonts = GetFonts();
            double yPosition = 40;

            gfx.DrawString($"Phase: {phase.Description}", fonts.Large, XBrushes.Black, 20, yPosition);
            yPosition += 30;

            decimal totalCost = 0;
            int totalMinutes = 0;

            foreach (var category in categories)
            {
                var times = await _mediator.Send(new FindTimesByCategoryIdQuery(category));
                if (!times.Any()) continue;

                yPosition = DrawCategoryHeader(gfx, category, fonts, yPosition);
                decimal categoryCost = DrawTimes(gfx, times, category, ref totalMinutes, ref totalCost, ref yPosition);

                gfx.DrawLine(new XPen(XColors.Gray, 1), 20, yPosition, page.Width - 20, yPosition);
                yPosition += 10;

                gfx.DrawString($"Total Cost for Category '{category.Name}': {categoryCost:C}", fonts.Small, XBrushes.Black, 20, yPosition);
                yPosition += 30;
            }

            DrawTotalSummary(gfx, totalMinutes, totalCost, fonts, ref yPosition);
            return document;
        }

        private (XFont Large, XFont Medium, XFont Small) GetFonts()
        {
            return (new XFont("Verdana", 20), new XFont("Verdana", 16), new XFont("Verdana", 14));
        }

        private double DrawCategoryHeader(XGraphics gfx, CategoryEntity category, (XFont Large, XFont Medium, XFont Small) fonts, double yPosition)
        {
            gfx.DrawString($"Category: {category.Name} | Hourly Rate: {category.PricePerHour:C} / hour", fonts.Medium, XBrushes.Black, 20, yPosition);
            yPosition += 10;

            gfx.DrawLine(new XPen(XColors.Gray, 1), 20, yPosition, gfx.PageSize.Width - 20, yPosition);
            yPosition += 10;

            return yPosition;
        }

        private decimal DrawTimes(XGraphics gfx, IEnumerable<TimeEntity> times, CategoryEntity category, ref int totalMinutes, ref decimal totalCost, ref double yPosition)
        {
            decimal categoryCost = 0;

            foreach (var time in times)
            {
                gfx.DrawString($"Time: {time.Title} - {time.TimeInMinutes} min", GetFonts().Small, XBrushes.Black, 20, yPosition);
                yPosition += 20;

                totalMinutes += time.TimeInMinutes;
                var timeCost = (category.PricePerHour ?? 0) * (time.TimeInMinutes / 60m);
                categoryCost += timeCost;
                totalCost += timeCost;
            }

            return categoryCost;
        }

        private void DrawTotalSummary(XGraphics gfx, int totalMinutes, decimal totalCost, (XFont Large, XFont Medium, XFont Small) fonts, ref double yPosition)
        {
            yPosition += 10; // Razmak ispred ukupne sume
            gfx.DrawLine(new XPen(XColors.Black, 2), 20, yPosition, gfx.PageSize.Width - 20, yPosition);
            yPosition += 10;

            var totalHours = totalMinutes / 60.0;
            gfx.DrawString($"Total Time: {totalHours:F2} hours", fonts.Medium, XBrushes.Black, 20, yPosition);
            yPosition += 25;

            gfx.DrawString($"Total Cost: {totalCost:C}", fonts.Medium, XBrushes.Black, 20, yPosition);
        }
    }
}
