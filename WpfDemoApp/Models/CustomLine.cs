using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using WpfDemoApp.Extensions;
using WpfDemoApp.Popups;

namespace WpfDemoApp.Models
{
    public class CustomLine : CustomShape
    {
        public string? A {  get; set; }
        public string? B { get; set; }

        public override void Render(Canvas canvas)
        {
            var line = new Line();

            var color = new Color()
            {
                A = byte.Parse(Color.Trim().Split(";")[0]),
                R = byte.Parse(Color.Trim().Split(";")[1]),
                G = byte.Parse(Color.Trim().Split(";")[2]),
                B = byte.Parse(Color.Trim().Split(";")[3]),
            };
            var customBrush = new SolidColorBrush(color);

            line.Stroke = customBrush;
            line.StrokeThickness = 2;

            line.X1 = double.Parse(A.Trim().Split(";")[0]);
            line.Y1 = double.Parse(A.Trim().Split(";")[1]);
            line.X2 = double.Parse(B.Trim().Split(";")[0]);
            line.Y2 = double.Parse(B.Trim().Split(";")[1]);

            line.MouseDown += new MouseButtonEventHandler(ShapesPopups.line_MouseDown);

            canvas.Children.Add(line);

            canvas.ScaleCanvas(double.Max(Math.Abs(line.Y1), Math.Abs(line.Y2)),
                   double.Max(Math.Abs(line.X1), Math.Abs(line.X2)),
                   (Line)LogicalTreeHelper.FindLogicalNode(canvas, "xLine"), (Line)LogicalTreeHelper.FindLogicalNode(canvas, "yLine"));
        }
    }
}
