using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WpfDemoApp.Models;

namespace WpfDemoApp.Converters
{
    public class JsonShapeConverter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(CustomShape).IsAssignableFrom(typeToConvert);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);

            string type = (string)jo["type"];

            CustomShape shape = null;

            switch(type)
            {
                case "line":
                    shape = new CustomLine();
                    break;
                case "circle":
                    shape = new CustomCircle();
                    break;
                case "triangle":
                    shape = new CustomTriangle();
                    break;
            }

            if(shape != null)
                serializer.Populate(jo.CreateReader(), shape);

            return shape;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
