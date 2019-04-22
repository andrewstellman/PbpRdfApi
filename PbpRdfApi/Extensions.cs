using System;
namespace PbpRdfApi
{
    public static class IriExtensions
    {
        /// <summary>
        /// Converts a string into a pbprdf: IRI <http://stellman-greene.com/pbprdf#>
        /// </summary>
        /// <returns>The pbprdf: IRI.</returns>
        /// <param name="iri">The string to convert.</param>
        public static string PbpRdfIri(this string iri) => iri.StartsWith("http://", StringComparison.CurrentCulture) 
            ? iri 
            : $"http://stellman-greene.com/pbprdf#{iri}";

        /// <summary>
        /// Strips the <http://stellman-greene.com/pbprdf#> prefix from an IRI.
        /// </summary>
        /// <returns>The IRI with the prefix stripped.</returns>
        /// <param name="iri">The IRI.</param>
        public static string StripPbpRdfPrefix(this string iri) =>
            iri.StartsWith("http://stellman-greene.com/pbprdf#", StringComparison.CurrentCulture)
            ? iri.Replace("http://stellman-greene.com/pbprdf#", "")
            : iri;
    }

    
}
