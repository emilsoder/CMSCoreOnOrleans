using System;
using System.Text.RegularExpressions;

namespace CMSCore.Content.Models
{
    public static class NormalizationExtensions
    {
        public static string NormalizeToSlug(this string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input)) return input;

                var arr = input.ToCharArray();
                arr = Array.FindAll(arr, c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c));
                return Regex.Replace(new string(arr), @"\s+", "-").ToLower().Normalize();
            }
            catch (Exception)
            {
                return input;
            }
        }
    }
}