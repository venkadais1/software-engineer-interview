namespace Zip.Installments.Core.Extensions
{
    /// <summary>
    ///     Convert list of error messages to string in fluent validation
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Convert list of string to Comma separated string
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string EnumToString(this IEnumerable<string> list) => string.Join(",", list.Select(x => x));
    }
}
