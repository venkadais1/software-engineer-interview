namespace Zip.Installments.Infrastructure.Extensions
{
    public static class CollectionExtensions
    {
        public static string EnumToString(this IEnumerable<string> list) => string.Join(",", list.Select(x => x));
    }
}
