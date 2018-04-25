using System.Collections.Generic;
using System.Linq;

namespace CMSCore.Shared.Abstractions.Extensions
{
    public static class RolesFormattingExtensions
    {
        public static string ArrayToCommaSeparatedString(this IList<string> strings)
        {
            return strings != null ? string.Join(',', strings?.Select(x => x)) : "";
        }

        public static string[] CommaSeparatedToArray(this string csvString) => csvString?.Split(',');
    }
}