namespace SmallLanguageModels
{
    public interface ILanguageModel
    {
        /// <summary>
        /// Provides the next token from the model given a particular
        /// previous sequence of tokens.
        /// </summary>
        /// <param name="token">A set of tokens</param>
        /// <returns>The next token in the set</returns>
        ulong NextToken(ulong[] tokens);

        /// <summary>
        /// Get the lookup table of all tokens in the model.
        /// </summary>
        /// <returns>A list of key-value pairs translating the ulong
        /// to a token.</returns>
        List<KeyValuePair<ulong, object>> GetTokenTable();

        /// <summary>
        /// Get the object associated with a particular token.
        /// </summary>
        /// <param name="token">The number associated with the
        /// token</param>
        /// <returns>The object associated with the token.</returns>
        object GetTokenValue(ulong token);

        /// <summary>
        /// Adds a token to the model and returns the number assigned to it.
        /// </summary>
        /// <param name="token">A token value to add to the model</param>
        /// <returns>The ulong associated with the token value.</returns>
        ulong AddTokenValue(object token);


        /// <summary>
        /// Train the model on a set of values.
        /// </summary>
        /// <param name="values">Provide a set of values
        /// to add to the model.</param>
        void Train(IEnumerable<object> values);

        /// <summary>
        /// Serialize the model out to a stream.
        /// </summary>
        /// <param name="stream">The stream to serialize to.</param>
        void SerializeModel(Stream stream);

        /// <summary>
        /// Load model from a stream.
        /// </summary>
        /// <param name="stream">The stream to deserialize from.</param>
        void DeserializeModel(Stream stream);

    }
}