using Newtonsoft.Json;
using System.IO;
using WpfDemoApp.Converters;
using WpfDemoApp.Models;

namespace WpfDemoApp.Readers
{
    public class JsonReader
    {
        public JsonReader() { }

        public List<CustomShape> ReadJsonFromFile(string path)
        {
            var shapes = new List<CustomShape>();

            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd(); 
                shapes = JsonConvert.DeserializeObject<List<CustomShape>>(json, new JsonShapeConverter());
            }

            return shapes;
        }
    }
}
