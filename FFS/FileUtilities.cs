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
    }
}