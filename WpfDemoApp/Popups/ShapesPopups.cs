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

namespace WpfDemoApp.Popups
{
    public static class ShapesPopups
    { 
        public static void line_MouseDown(object sender, MouseButtonEventArgs e)
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

        public static void circle_MouseDown(object sender, MouseButtonEventArgs e)
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

        public static void polygon_MouseDown(object sender, MouseButtonEventArgs e)
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
