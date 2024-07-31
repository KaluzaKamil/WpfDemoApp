using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfDemoApp.Models;
using WpfDemoApp.Readers;

namespace WpfDemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Line _xLine;
        private Line _yLine;
        public MainWindow()
        {
            _xLine = new Line()
            {
                X1 = 0,
                X2 = 0,
                Y1 = 250,
                Y2 = -250,
                Name = "xLine",
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            _yLine = new Line()
            {
                X1 = 250,
                X2 = -250,
                Y1 = 0,
                Y2 = 0,
                Name = "yLine",
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            InitializeComponent();

            canvas.Children.Add(_xLine);
            canvas.Children.Add(_yLine);
        }

        private void Select_Button_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            string filePath;

            openFileDialog.DefaultExt = ".json";
            openFileDialog.Filter = "JSON Files (*.json)|*.json";

            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                filePath = openFileDialog.FileName;

                var reader = new JsonReader();

                var shapes = reader.ReadJsonFromFile(filePath);

                foreach (var shape in shapes)
                {
                    switch(shape.Type)
                    {
                        case "line":
                            DrawLine((CustomLine)shape);
                            break;
                        case "circle":
                            DrawCircle((CustomCircle)shape);
                            break;
                        case "triangle":
                            DrawTriangle((CustomTriangle)shape);
                            break;
                    }
                }
            }
        }

        private void DrawLine(CustomLine cLine)
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

            ScaleCanvas(double.Max(Math.Abs(line.Y1), Math.Abs(line.Y2)),
                   double.Max(Math.Abs(line.X1), Math.Abs(line.X2)));
        }

        private void DrawCircle(CustomCircle cCircle)
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

            ScaleCanvas(Math.Abs(centerY + circle.Height), Math.Abs(centerX - circle.Width));
        }

        private void DrawTriangle(CustomTriangle cTriangle)
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
            if(cTriangle.Filled)
                polygon.Fill = customBrush;
            polygon.StrokeThickness = 2;

            canvas.Children.Add(polygon);

            var biggestXPoint = polygon.Points.Aggregate((p1, p2) => Math.Abs(p1.X) > Math.Abs(p2.X) ? p1 : p2);
            var biggestYPoint = polygon.Points.Aggregate((p1, p2) => Math.Abs(p1.Y) > Math.Abs(p2.Y) ? p1 : p2);
            ScaleCanvas(biggestYPoint.Y, biggestXPoint.X);
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            canvas.Height = 25;
            canvas.Width = 25;

            canvas.Children.Add(_xLine);
            canvas.Children.Add(_yLine);
        }

        private void ScaleCanvas(double height, double width)
        {
            var biggerValue = double.Max(height, width);
            if (canvas.Height < biggerValue / 2)
            {
                canvas.Height = biggerValue / 2;
                canvas.Width = biggerValue / 2;

                canvas.Children.Remove(_xLine);
                canvas.Children.Remove(_yLine);

                _yLine.X1 = 2 * biggerValue;
                _yLine.X2 = 2 * biggerValue * -1;
                _xLine.Y1 = 2 * biggerValue;
                _xLine.Y2 = 2 * biggerValue * -1;

                canvas.Children.Add(_xLine);
                canvas.Children.Add(_yLine);    
            }
        }
    }
}