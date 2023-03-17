namespace Polokus.Core.Interfaces.Execution
{
    /// <summary>
    /// A 'bag' of global variables for concrete process instance.
    /// </summary>
    public interface IScriptVariables
    {
        /// <summary>
        /// Returns all values.
        /// </summary>
        List<object> Values { get; }

        /// <summary>
        /// Returns all names of variables.
        /// </summary>
        List<string> Variables { get; }

        /// <summary>
        /// Returns typed value of variable with given name <paramref name="variable"/>.
        /// If not found or impossible cast to <typeparamref name="T"/> then returns null.
        /// </summary>
        /// <typeparam name="T">Requested type.</typeparam>
        /// <param name="variable">Name of variable.</param>
        /// <returns></returns>
        T? TryGetValue<T>(string variable);

        /// <summary>
        /// Sets the value of global variable. If not exists, add the variable.
        /// </summary>
        /// <param name="variable">Name of variable.</param>
        /// <param name="value">Value of variable.</param>
        void SetValue(string variable, object value);

        /// <summary>
        /// Gets value, not typed. If not found value with <paramref name="variable"/> throws exception.
        /// </summary>
        /// <param name="variable">Name of variable.</param>
        object GetValue(string variable);

        /// <summary>
        /// Gets value, typed. If not found value with the name or impossible cast - throws exception.
        /// </summary>
        /// <typeparam name="T">Expected type of variable.</typeparam>
        /// <param name="variable">Name of variable.</param>
        /// <returns></returns>
        T GetValue<T>(string variable);

        /// <summary>
        /// Returns globals dictionary as string with json object.
        /// </summary>
        string GetJson();

        /// <summary>
        /// Replaces old global values with new dictionary <paramref name="values"/>.
        /// </summary>
        /// <param name="values">Dictionary with new global values.</param>
        void SetValues(IDictionary<string, object> values);
    }
}
