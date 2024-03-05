
namespace BeGiftee.Source.Helpers
{
    public class UrlParamsHelper
    {

        /// <summary>
        /// Replaces placeholders in the format $X in the URL template with the specified parameters.
        /// </summary>
        /// <param name="urlTemplate">The URL template containing placeholders in the format $X, where X is a one-based index corresponding to the parameters array.</param>
        /// <param name="parameters">An array of strings containing the parameters to replace in the URL template.</param>
        /// <returns>A string with the placeholders replaced by the corresponding parameters.</returns>
        /// <remarks>
        /// This method dynamically adjusts to the number of parameters provided, allowing for flexible usage. 
        /// Placeholders are replaced in a one-based index manner, meaning $1 refers to the first element in the parameters array, $2 to the second, and so on.
        /// </remarks>
        public static string GetUrlWithParams(string urlTemplate, params string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                return urlTemplate;
            }

            for (int i = 1; i <= parameters.Length; i++)
            {
                urlTemplate = urlTemplate.Replace($"${i}", $"{{{i - 1}}}");
            }

            return string.Format(urlTemplate, parameters);
        }
    }
}
