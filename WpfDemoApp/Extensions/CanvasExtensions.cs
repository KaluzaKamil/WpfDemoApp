using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WpfDemoApp.Extensions
{
    public static class CanvasExtensions
    {
        public static Canvas ScaleCanvas(this Canvas canvas, double height, double width, Line xLine, Line yLine)
        {
            var biggerValue = double.Max(height, width);
            if (canvas.Height < biggerValue / 2)
            {
                canvas.Height = biggerValue / 2;
                canvas.Width = biggerValue / 2;

                yLine.X1 = 2 * biggerValue;
                yLine.X2 = 2 * biggerValue * -1;
                xLine.Y1 = 2 * biggerValue;
                xLine.Y2 = 2 * biggerValue * -1;

                canvas.Children.Remove(xLine);
                canvas.Children.Remove(yLine);
                canvas.Children.Add(xLine);
                canvas.Children.Add(yLine);
            }

            return canvas;
        }
    }
}
