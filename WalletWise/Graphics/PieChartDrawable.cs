// Graphics/PieChartDrawable.cs
using WalletWise.ViewModels;

namespace WalletWise.Graphics;

public class PieChartDrawable : IDrawable
{
    public List<ChartLegendItem> Items { get; set; } = [];

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float startAngle = 0;
        float total = 0;
        foreach (var item in Items)
        {
            total += (float)item.Value;
        }

        if (total == 0) return;

        // Calcola il centro e il raggio del cerchio
        float centerX = dirtyRect.Center.X;
        float centerY = dirtyRect.Center.Y;
        float radius = (float)(Math.Min(dirtyRect.Width, dirtyRect.Height) / 2 * 0.9);

        foreach (var item in Items)
        {
            float sweepAngle = 360f * ((float)item.Value / total);

            canvas.FillColor = item.Color;
            canvas.FillArc(centerX - radius, centerY - radius, radius * 2, radius * 2, startAngle, startAngle + sweepAngle, true);

            startAngle += sweepAngle;
        }
    }
}