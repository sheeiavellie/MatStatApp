using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

namespace MatStatApp.Models.Charts
{
    internal class Gistogramm<X>
    {
        private static readonly SKColor s_gray = new(195, 195, 195);
        private static readonly SKColor s_dark3 = new(255, 255, 255);

        public DrawMarginFrame Frame { get; set; } =
            new()
            {
                Fill = new SolidColorPaint(s_dark3),
                Stroke = new SolidColorPaint
                {
                    Color = s_gray,
                    StrokeThickness = 1
                }
            };
        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public Gistogramm(string x_name, string y_name, X[] x_vals, string[] y_vals)
        {
            Series = new ISeries[]
            {
                new ColumnSeries<X>
                {
                    Values = x_vals
                }
            };

            XAxes = new Axis[]
            {
                new Axis
                {
                    Name = x_name,
                    TextSize = 18,
                    Padding = new Padding(5, 15, 5, 5),
                    SeparatorsPaint = new SolidColorPaint
                    {
                        Color = s_gray,
                        StrokeThickness = 1,
                        PathEffect = new DashEffect(new float[] { 3, 3 })
                    },                    
                    TicksPaint = new SolidColorPaint
                    {
                        Color = s_gray,
                        StrokeThickness = 1.5f
                    },
                    SubticksPaint = new SolidColorPaint
                    {
                        Color = s_gray,
                        StrokeThickness = 1
                    }
                }
            };

            YAxes = new Axis[]
            {
                new Axis
                {
                    Name = y_name,
                    TextSize = 18,
                    Padding = new Padding(5, 0, 15, 0),
                    SeparatorsPaint = new SolidColorPaint
                    {
                        Color = s_gray,
                        StrokeThickness = 1,
                        PathEffect = new DashEffect(new float[] { 3, 3 })
                    },                    
                    TicksPaint = new SolidColorPaint
                    {
                        Color = s_gray,
                        StrokeThickness = 1.5f
                    },
                    SubticksPaint = new SolidColorPaint
                    {
                        Color = s_gray,
                        StrokeThickness = 1
                    }
                }
            };

            XAxes[0].Labels = y_vals;
        }
    }
}
