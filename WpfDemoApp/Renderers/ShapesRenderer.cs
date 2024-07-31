using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfDemoApp.Extensions;
using WpfDemoApp.Models;

namespace WpfDemoApp.Renderers
{
    public class ShapesRenderer
    {
        public void RenderLine(CustomLine cLine, Canvas canvas)
        {
            var line = new Line();

            var color = new Color()
            {
                A = byte.Parse(cLine.Color.Trim().Split(";")[0]),
                R = byte.Parse(cLine.Color.Trim().Split(";")[1]),
                G = byte.Parse(cLine.Color.Trim().Split(";")[2]),
                B = byte.Parse(cLine.Color.Trim().Split(";")[3]),
            };
            var customBrush = new SolidColorBrush(color);

            line.Stroke = customBrush;
            line.StrokeThickness = 2;

            line.X1 = double.Parse(cLine.A.Trim().Split(";")[0]);
            line.Y1 = double.Parse(cLine.A.Trim().Split(";")[1]);
            line.X2 = double.Parse(cLine.B.Trim().Split(";")[0]);
            line.Y2 = double.Parse(cLine.B.Trim().Split(";")[1]);

            canvas.Children.Add(line);

            canvas.ScaleCanvas(double.Max(Math.Abs(line.Y1), Math.Abs(line.Y2)),
                   double.Max(Math.Abs(line.X1), Math.Abs(line.X2)),
                   (Line)LogicalTreeHelper.FindLogicalNode(canvas, "xLine"), (Line)LogicalTreeHelper.FindLogicalNode(canvas, "yLine"));
        }

        public void RenderCircle(CustomCircle cCircle, Canvas canvas)
        {
            var circle = new Ellipse();

            circle.Width = cCircle.Radius;
            circle.Height = cCircle.Radius;

            var color = new Color()
            {
                A = byte.Parse(cCircle.Color.Trim().Split(";")[0]),
                R = byte.Parse(cCircle.Color.Trim().Split(";")[1]),
                G = byte.Parse(cCircle.Color.Trim().Split(";")[2]),
                B = byte.Parse(cCircle.Color.Trim().Split(";")[3]),
            };
            var customBrush = new SolidColorBrush(color);

            circle.Stroke = customBrush;
            if (cCircle.Filled)
                circle.Fill = customBrush;
            circle.StrokeThickness = 2;

            canvas.Children.Add(circle);

            var centerX = double.Parse(cCircle.Center.Trim().Split(";")[0]);
            var centerY = double.Parse(cCircle.Center.Trim().Split(";")[1]);
            circle.SetValue(Canvas.LeftProperty, centerX - (cCircle.Radius / 2));
            circle.SetValue(Canvas.TopProperty, centerY - (cCircle.Radius / 2));

            canvas.ScaleCanvas(Math.Abs(centerY + circle.Height), Math.Abs(centerX - circle.Width), (Line)LogicalTreeHelper.FindLogicalNode(canvas, "xLine"), (Line)LogicalTreeHelper.FindLogicalNode(canvas, "yLine"));
        }

        public void RenderTriangle(CustomTriangle cTriangle, Canvas canvas)
        {
            var polygon = new Polygon();

            polygon.Points.Add(new Point(double.Parse(cTriangle.A.Trim().Split(";")[0]), double.Parse(cTriangle.A.Trim().Split(";")[1])));
            polygon.Points.Add(new Point(double.Parse(cTriangle.B.Trim().Split(";")[0]), double.Parse(cTriangle.B.Trim().Split(";")[1])));
            polygon.Points.Add(new Point(double.Parse(cTriangle.C.Trim().Split(";")[0]), double.Parse(cTriangle.C.Trim().Split(";")[1])));

            var color = new Color()
            {
                A = byte.Parse(cTriangle.Color.Trim().Split(";")[0]),
                R = byte.Parse(cTriangle.Color.Trim().Split(";")[1]),
                G = byte.Parse(cTriangle.Color.Trim().Split(";")[2]),
                B = byte.Parse(cTriangle.Color.Trim().Split(";")[3]),
            };
            var customBrush = new SolidColorBrush(color);

            polygon.Stroke = customBrush;
            if (cTriangle.Filled)
                polygon.Fill = customBrush;
            polygon.StrokeThickness = 2;

            canvas.Children.Add(polygon);

            var biggestXPoint = polygon.Points.Aggregate((p1, p2) => Math.Abs(p1.X) > Math.Abs(p2.X) ? p1 : p2);
            var biggestYPoint = polygon.Points.Aggregate((p1, p2) => Math.Abs(p1.Y) > Math.Abs(p2.Y) ? p1 : p2);
            canvas.ScaleCanvas(biggestYPoint.Y, biggestXPoint.X, (Line)LogicalTreeHelper.FindLogicalNode(canvas, "xLine"), (Line)LogicalTreeHelper.FindLogicalNode(canvas, "yLine"));
        }
    }
}
