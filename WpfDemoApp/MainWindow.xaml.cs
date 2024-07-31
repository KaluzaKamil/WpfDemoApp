using Microsoft.Win32;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfDemoApp.Models;
using WpfDemoApp.Readers;
using WpfDemoApp.Renderers;

namespace WpfDemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Line _xLine;
        private Line _yLine;
        private ShapesRenderer _renderer;
        public MainWindow()
        {
            _renderer = new ShapesRenderer();
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
                            _renderer.RenderLine((CustomLine)shape, canvas);
                            break;
                        case "circle":
                            _renderer.RenderCircle((CustomCircle)shape, canvas);
                            break;
                        case "triangle":
                            _renderer.RenderTriangle((CustomTriangle)shape, canvas);
                            break;
                    }
                }
            }
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            canvas.Height = 25;
            canvas.Width = 25;

            canvas.Children.Add(_xLine);
            canvas.Children.Add(_yLine);
        }
    }
}