namespace Polokus.Core.Interfaces.Execution
{
    public interface IScriptProvider
    {
        /// <summary>
        /// Collection of variables.
        /// </summary>
        IScriptVariables Globals { get; }

        /// <summary>
        /// Converts a string that has been HTML-encoded for HTTP transmission into a decoded string.
        /// Example: '&amp;gt;' -> '>' , '&amp;lt' -> '&lt;'
        /// </summary>
        /// <param name="script">Script to decode.</param>
        string Decode(string script);

        /// <summary>
        /// This method runs script dynamically and returns result as type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="script">Script to evaluate.</param>
        Task<T> EvalScriptAsync<T>(string script);

        /// <summary>
        /// This method runs script dynamically.
        /// </summary>
        /// <param name="script">Script to evaluate.</param>
        Task EvalScriptAsync(string script);

        /// <summary>
        /// Decides if given string is valid variable name (for outgoing edges).
        /// For example: '$x' is valid, '$x + abc' is invalid.
        /// </summary>
        /// <param name="str">Variable name with '$'.</param>
        bool IsValidOutgoingVariable(string str);

    }
}
