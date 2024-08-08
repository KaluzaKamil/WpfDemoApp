using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using WpfDemoApp.Popups;
using WpfDemoApp.Extensions;

namespace WpfDemoApp.Models
{
    public class CustomTriangle : CustomShape
    {
        public string? A { get; set; }
        public string? B { get; set; }
        public string? C { get; set; }
        public bool Filled { get; set; }

        public override void Render(Canvas canvas)
        {
            var polygon = new Polygon();

            polygon.Points.Add(new Point(double.Parse(A.Trim().Split(";")[0]), double.Parse(A.Trim().Split(";")[1])));
            polygon.Points.Add(new Point(double.Parse(B.Trim().Split(";")[0]), double.Parse(B.Trim().Split(";")[1])));
            polygon.Points.Add(new Point(double.Parse(C.Trim().Split(";")[0]), double.Parse(C.Trim().Split(";")[1])));

            var color = new Color()
            {
                A = byte.Parse(Color.Trim().Split(";")[0]),
                R = byte.Parse(Color.Trim().Split(";")[1]),
                G = byte.Parse(Color.Trim().Split(";")[2]),
                B = byte.Parse(Color.Trim().Split(";")[3]),
            };
            var customBrush = new SolidColorBrush(color);

            polygon.Stroke = customBrush;
            if (Filled)
                polygon.Fill = customBrush;
            polygon.StrokeThickness = 2;

            polygon.MouseDown += new MouseButtonEventHandler(ShapesPopups.polygon_MouseDown);

            canvas.Children.Add(polygon);

            var biggestXPoint = polygon.Points.Aggregate((p1, p2) => Math.Abs(p1.X) > Math.Abs(p2.X) ? p1 : p2);
            var biggestYPoint = polygon.Points.Aggregate((p1, p2) => Math.Abs(p1.Y) > Math.Abs(p2.Y) ? p1 : p2);
            canvas.ScaleCanvas(biggestYPoint.Y, biggestXPoint.X, (Line)LogicalTreeHelper.FindLogicalNode(canvas, "xLine"), (Line)LogicalTreeHelper.FindLogicalNode(canvas, "yLine"));
        }
    }
}
