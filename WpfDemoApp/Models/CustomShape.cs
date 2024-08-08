using System.Windows.Controls;

namespace WpfDemoApp.Models
{
    public abstract class CustomShape
    {
        public string? Type { get; set; }
        public string? Color { get; set; }

        public abstract void Render(Canvas canvas);
    }
}
