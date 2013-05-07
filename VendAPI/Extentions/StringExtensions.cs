namespace VendAPI.Extentions
{
    using System.Text;

    public static class StringExtensions
    {
        public static string Join(this string[] array, char separator)
        {
            var sb = new StringBuilder();
            foreach (var element in array)
            {
                sb.Append(element + separator);
            }

            return sb.ToString().TrimEnd(separator);
        }
    }
}
