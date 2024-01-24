using System.Linq;

namespace App.Base.Extensions
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var snakeCase = string.Concat(input.Select((x, i) =>
            {
                if (i > 0 && char.IsUpper(x))
                {
                    return "_" + char.ToLower(x);
                }
                else if (char.IsWhiteSpace(x))
                {
                    return "_";
                }

                return char.ToLower(x).ToString();
            }));

            return snakeCase;
        }

        public static string IgnoreCase(this string str)
        {
            return str.Trim().ToLower();
        }

        public static string Or(this string str, string or)
        {
            return string.IsNullOrEmpty(str) ? or : str;
        }

        public static string ValueOrProvided(this string str, string defaultValue = "")
            => string.IsNullOrEmpty(str) ? defaultValue : str;

        public static string ValueOrNull(this string str)
            => str.ValueOrProvided(null);
    }
}