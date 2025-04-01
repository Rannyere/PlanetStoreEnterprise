using System.Linq;

namespace PSE.Core.Utils;

public static class StringUtils
{
    public static string OnlyNumbers(this string str, string input)
    {
        return new string(input.Where(char.IsDigit).ToArray());
    }
}