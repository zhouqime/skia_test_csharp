using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp.Views.Forms;
using System.IO;


namespace skia_mac
{
    public partial class MainPage : ContentPage
    {
        List<Line> lines = new List<Line>();
        public MainPage()
        {
            InitializeComponent();
            Random r = new Random();
            for (int i = 0; i < 100000; i++)
            {
                lines.Add(new Line(1000 * 2, 800 * 2, Math.PI / 4, r));
            }
            this.view.PaintSurface += View_PaintSurface;

        }



        private void View_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear(new SkiaSharp.SKColor(255, 255, 255));

            DateTime time = DateTime.Now;

            var paint = new SkiaSharp.SKPaint();
            paint.Style =  SkiaSharp.SKPaintStyle.Stroke;
            paint.IsAntialias = true;
            paint.StrokeCap = SkiaSharp.SKStrokeCap.Round;

            foreach (var l in lines)
            {
                paint.ColorF = l.color;
                paint.StrokeWidth = (float)l.w;
                canvas.DrawLine(l.x1, l.y1, l.x2, l.y2, paint);
            }
            canvas.Flush();
            surface.Snapshot();

            this.info.Text = (DateTime.Now - time).Milliseconds.ToString();
            Console.WriteLine((DateTime.Now - time).Milliseconds.ToString());
            
        }

    }

    class Line
    {
        public float x1, x2, y1, y2, w;
        public SkiaSharp.SKColorF color;
        public Line(double w,double h,double angle,Random random) {
            this.x1 = (float)(random.NextDouble() * w);
            this.y1 = (float)(random.NextDouble() * h);

            var l = (random.NextDouble() - 0.5) * w;
            this.x2 = (float)(this.x1 + l * Math.Cos(angle));
            this.y2 = (float)(this.y1 + l * Math.Sin(angle));

            this.color = new SkiaSharp.SKColorF(
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble()
                );

            this.w = (float)(2.0 + random.NextDouble() * 40);
        }
    }
}
