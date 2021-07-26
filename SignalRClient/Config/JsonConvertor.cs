using Client.Entity;
using Newtonsoft.Json;
using System.IO;

namespace Client
{
    public static class JsonConvertor
    {
        public static string DataToJsan(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
        public static ServerConfig ReadFromJson(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {

                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<ServerConfig>(json);
            }
        }

       
    }
}
