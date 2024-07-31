using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            CustomShape shape;

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
                default:
                    throw new Exception("Type not found");
            }

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
