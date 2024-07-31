using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfDemoApp.Extensions;
using WpfDemoApp.Models;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Text;

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

            line.MouseDown += new MouseButtonEventHandler(line_MouseDown);

            canvas.Children.Add(line);

            canvas.ScaleCanvas(double.Max(Math.Abs(line.Y1), Math.Abs(line.Y2)),
                   double.Max(Math.Abs(line.X1), Math.Abs(line.X2)),
                   (Line)LogicalTreeHelper.FindLogicalNode(canvas, "xLine"), (Line)LogicalTreeHelper.FindLogicalNode(canvas, "yLine"));
        }

        private void line_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var line = sender as Line;
            if (line != null)
            {
                var popup = new Popup();
                var grid = new Grid();

                grid.Background = Brushes.White;
                var sBuilder = new StringBuilder();

                sBuilder.AppendLine("X1: " + line.X1.ToString());
                sBuilder.AppendLine("Y1: " + line.Y1.ToString());
                sBuilder.AppendLine("X2: " + line.X2.ToString());
                sBuilder.AppendLine("Y2: " + line.Y2.ToString());

                var border = new Border();
                border.BorderThickness = new Thickness(1, 1, 1, 1);
                border.BorderBrush = Brushes.Black;

                var text = new TextBlock()
                {
                    Text = sBuilder.ToString()
                };

                border.Child = text;

                grid.Children.Add(border);
                popup.Child = grid;

                popup.PlacementTarget = line;
                popup.Placement = PlacementMode.Right;
                popup.Visibility = Visibility.Visible;
                popup.StaysOpen = false;
                popup.IsOpen = true;
            }
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

            circle.MouseDown += new MouseButtonEventHandler(circle_MouseDown);

            canvas.Children.Add(circle);

            var centerX = double.Parse(cCircle.Center.Trim().Split(";")[0]);
            var centerY = double.Parse(cCircle.Center.Trim().Split(";")[1]);
            circle.SetValue(Canvas.LeftProperty, centerX - (cCircle.Radius / 2));
            circle.SetValue(Canvas.TopProperty, centerY - (cCircle.Radius / 2));

            canvas.ScaleCanvas(Math.Abs(centerY + circle.Height), Math.Abs(centerX - circle.Width), (Line)LogicalTreeHelper.FindLogicalNode(canvas, "xLine"), (Line)LogicalTreeHelper.FindLogicalNode(canvas, "yLine"));
        }

        private void circle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var circle = sender as Ellipse;
            var popup = new Popup();
            var grid = new Grid();

            grid.Background = Brushes.White;
            var sBuilder = new StringBuilder();

            sBuilder.AppendLine("Radius: " + circle.Width.ToString());
            sBuilder.AppendLine("Center: " + circle.GetValue(Canvas.LeftProperty).ToString() + ", " + circle.GetValue(Canvas.TopProperty).ToString());

            if (circle.Fill != null)
                sBuilder.AppendLine("\nFill: " + circle.Fill.ToString());
            else
                sBuilder.AppendLine("\nFill: none");

            var border = new Border();
            border.BorderThickness = new Thickness(1, 1, 1, 1);
            border.BorderBrush = Brushes.Black;

            var text = new TextBlock()
            {
                Text = sBuilder.ToString()
            };

            border.Child = text;

            grid.Children.Add(border);
            popup.Child = grid;

            popup.PlacementTarget = circle;
            popup.Placement = PlacementMode.Right;
            popup.Visibility = Visibility.Visible;
            popup.StaysOpen = false;
            popup.IsOpen = true;
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

            polygon.MouseDown += new MouseButtonEventHandler(polygon_MouseDown);

            canvas.Children.Add(polygon);

            var biggestXPoint = polygon.Points.Aggregate((p1, p2) => Math.Abs(p1.X) > Math.Abs(p2.X) ? p1 : p2);
            var biggestYPoint = polygon.Points.Aggregate((p1, p2) => Math.Abs(p1.Y) > Math.Abs(p2.Y) ? p1 : p2);
            canvas.ScaleCanvas(biggestYPoint.Y, biggestXPoint.X, (Line)LogicalTreeHelper.FindLogicalNode(canvas, "xLine"), (Line)LogicalTreeHelper.FindLogicalNode(canvas, "yLine"));
        }

        private void polygon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var polygon = sender as Polygon;
            var popup = new Popup();
            var grid = new Grid();

            grid.Background = Brushes.White;
            var sBuilder = new StringBuilder();

            sBuilder.AppendLine("Points: ");
            foreach(var point in polygon.Points)
            {
                sBuilder.Append("(" + point.X.ToString() + ", " + point.Y.ToString() + ")");
            }

            if (polygon.Fill != null)
                sBuilder.AppendLine("\nFill: " + polygon.Fill.ToString());
            else
                sBuilder.AppendLine("\nFill: none");

            var border = new Border();
            border.BorderThickness = new Thickness(1, 1, 1, 1);
            border.BorderBrush = Brushes.Black;

            var text = new TextBlock()
            {
                Text = sBuilder.ToString()
            };

            border.Child = text;

            grid.Children.Add(border);
            popup.Child = grid;

            popup.PlacementTarget = polygon;
            popup.Placement = PlacementMode.Right;
            popup.Visibility = Visibility.Visible;
            popup.StaysOpen = false;
            popup.IsOpen = true;
        }
    }
}
