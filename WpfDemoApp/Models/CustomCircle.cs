using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using WpfDemoApp.Popups;
using WpfDemoApp.Extensions;

namespace WpfDemoApp.Models
{
    public class CustomCircle : CustomShape
    {
        public string? Center { get; set; }
        public double Radius { get; set; }
        public bool Filled { get; set; }

        public override void Render(Canvas canvas)
        {
            var circle = new Ellipse();

            circle.Width = Radius;
            circle.Height = Radius;

            var color = new Color()
            {
                A = byte.Parse(Color.Trim().Split(";")[0]),
                R = byte.Parse(Color.Trim().Split(";")[1]),
                G = byte.Parse(Color.Trim().Split(";")[2]),
                B = byte.Parse(Color.Trim().Split(";")[3]),
            };
            var customBrush = new SolidColorBrush(color);

            circle.Stroke = customBrush;
            if (Filled)
                circle.Fill = customBrush;
            circle.StrokeThickness = 2;

            circle.MouseDown += new MouseButtonEventHandler(ShapesPopups.circle_MouseDown);

            canvas.Children.Add(circle);

            var centerX = double.Parse(Center.Trim().Split(";")[0]);
            var centerY = double.Parse(Center.Trim().Split(";")[1]);
            circle.SetValue(Canvas.LeftProperty, centerX - (Radius / 2));
            circle.SetValue(Canvas.TopProperty, centerY - (Radius / 2));

            canvas.ScaleCanvas(Math.Abs(centerY + circle.Height), Math.Abs(centerX - circle.Width), (Line)LogicalTreeHelper.FindLogicalNode(canvas, "xLine"), (Line)LogicalTreeHelper.FindLogicalNode(canvas, "yLine"));
        }
    }
}
