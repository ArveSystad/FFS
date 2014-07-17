using System.Linq;

namespace FFS
{
    public class FileUtilities
    {
        public static string GetType(string path)
        {
            var strings = path.Split(new [] { '.' });
            if (strings.Length == 1)
                return null;
            return strings.Last();
        }

        public static string GetMimetype(string type)
        {
            switch (type)
            {
                case null:
                    return "text/html";
                case "css":
                    return "text/css";
                default:
                    return "text/plain";
            }
        }
    }
}