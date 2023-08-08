using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallLanguageModels
{
    internal interface ITokenizer
    {
        /// <summary>
        /// Tokenize byte array.
        /// </summary>
        /// <param name="b">Array of bytes</param>
        /// <returns>IEnumerable of token values.</returns>Z
        public IEnumerable<object> GetTokens(byte[] b);
        /// <summary>
        /// Tokenize stream.
        /// </summary>
        /// <param name="b">Stream to be tokenized</param>
        /// <returns>IEnumerable of token values.</returns>Z
        public IEnumerable<object> GetTokens(Stream s);
        /// <summary>
        /// Set of options to configure ITokenizer.
        /// </summary>
        public KeyValuePair<string,string> Options { get; set; }

    }
}
