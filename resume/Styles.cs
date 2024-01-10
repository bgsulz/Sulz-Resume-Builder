using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SkiaSharp;

namespace BGSulz;

public static class Styles
{
    public const string SulzYellow = "ffe186";

    public const int RectMargin = 7;

    public static IContainer RoundedRect(this IContainer container, string color = SulzYellow, bool hasStroke = true, string strokeColor = "000000")
    {
        container.Canvas((canvas, size) => DrawRoundedRectangle(canvas, size, color, hasStroke, strokeColor));
        return container;
    }
    
    private static void DrawRoundedRectangle(SKCanvas canvas, Size size, string color, bool hasStroke, string strokeColor)
    {
        using var paint = new SKPaint
        {
            Color = SKColor.Parse(color),
            IsStroke = false,
            StrokeWidth = 1,
            IsAntialias = true
        };
        
        canvas.DrawRoundRect(0, 0, size.Width, size.Height, 2, 2, paint);

        if (!hasStroke) return;
        
        using var stroke = new SKPaint
        {
            Color = SKColor.Parse(strokeColor),
            IsStroke = true,
            StrokeWidth = 1,
            IsAntialias = true
        };
        
        canvas.DrawRoundRect(0, 0, size.Width, size.Height, 2, 2, stroke);
    }
}