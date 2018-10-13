using System.Linq;

namespace ReaperParser
{
    public static class Extensions
    {
        public static bool StartsWith(this string s, char c)
        {
            return s.Any() && s[0] == c;
        }
        
    }
}
