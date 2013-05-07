namespace VendAPI.Extentions
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public static class JsonExtention
    {
        public static string ToJson<T>(this T parent)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var tempStream = new MemoryStream())
            {
                serializer.WriteObject(tempStream, parent);
                return Encoding.Default.GetString(tempStream.ToArray());
            }
        }

        public static T FromJson<T>(this string json)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var tempStream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                return (T)serializer.ReadObject(tempStream);
            }
        }
    }
}
