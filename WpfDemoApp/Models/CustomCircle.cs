using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemoApp.Models
{
    public class CustomCircle : CustomShape
    {
        public string Center { get; set; }
        public double Radius { get; set; }
        public bool Filled { get; set; }
    }
}
