using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatStatApp.Models.Charts
{
    internal class EmpFunction
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

        public EmpFunction(Dictionary<double, double[]> data, List<Tuple<double, double>> theor_data)
        {
            var set = new List<LineSeries<ObservablePoint>>();
            foreach (var series in data)
            {
                set.Add(new LineSeries<ObservablePoint>
                {
                    Values = new ObservablePoint[]
                    {
                        new ObservablePoint(series.Value[0], series.Key),
                        new ObservablePoint(series.Value[1], series.Key),
                    },
                    Stroke = new SolidColorPaint
                    {
                        Color = SKColors.Green,
                        StrokeCap = SKStrokeCap.Round,
                        //StrokeThickness = 1.5F,
                    },
                    GeometrySize = 0,
                    Fill = null
                });
            }

            var point = new List<ObservablePoint>();

            foreach(var e in theor_data)
            {
                point.Add(new ObservablePoint(e.Item1, e.Item2));
            }

            set.Add(new LineSeries<ObservablePoint>
            {
                Values = point.ToArray(),
                GeometrySize = 0,
                LineSmoothness = 1,
                Stroke = new SolidColorPaint
                {
                    Color = SKColors.Red
                }
            });

            Series = set.ToArray();

            XAxes = new Axis[]
            {
                new Axis
                {
                    NameTextSize = 15,
                    Padding = new Padding(5, 15, 5, 5),
                    MinStep = 1,
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
                    NameTextSize = 15,
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
        }
    }
}
