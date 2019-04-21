using System;
namespace PbpRdfApi
{
    public static class IriExtensions
    {
        /// <summary>
        /// Converts a string into a pbprdf: IRI <http://stellman-greene.com/pbprdf#>
        /// </summary>
        /// <returns>The pbprdf: IRI.</returns>
        /// <param name="s">The string to convert.</param>
        public static string PbpRdfIri(this string s) => $"http://stellman-greene.com/pbprdf#{s}";
    }

    
}
