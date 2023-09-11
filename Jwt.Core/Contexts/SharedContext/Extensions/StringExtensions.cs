using System.Text;

namespace Jwt.Core.Contexts.SharedContext.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64(this string str) 
            => Convert.ToBase64String(Encoding.ASCII.GetBytes(str));
    }
}
