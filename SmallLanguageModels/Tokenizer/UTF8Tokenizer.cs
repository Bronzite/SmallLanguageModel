using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallLanguageModels.Tokenizer
{
    public class UTF8Tokenizer : ITokenizer
    {
        public KeyValuePair<string, string> Options { get; set; }

        public IEnumerable<object> GetTokens(byte[] b)
        {
            MemoryStream memoryStream = new MemoryStream(b);
            return ((ITokenizer)this).GetTokens(memoryStream);
        }

        public IEnumerable<object> GetTokens(Stream s)
        {
            List<byte> lstByte = new List<byte>();
            byte[] buffer = new byte[4096];
            int iBufferRead = 100;
            while(iBufferRead>0)
            {
                iBufferRead = s.Read(buffer,0, buffer.Length);
                lstByte.AddRange(buffer.Take(iBufferRead));
            }

            string sUTF8 = UTF8Encoding.UTF8.GetString(lstByte.ToArray());

            return GetTokens(sUTF8);
        }

        public List<string> GetTokens(string sUTF8)
        {
            List<string> retval = new List<string>();

            string sToken = "";
            foreach (char c in sUTF8)
            {
                if (char.IsLetter(c)) sToken += c;
                if (char.IsPunctuation(c) || char.IsDigit(c) || char.IsWhiteSpace(c))
                {
                    if (sToken != "")
                    {
                        retval.Add(sToken);
                        retval.Add(c.ToString());
                        sToken = "";

                    }
                }
                if (!char.IsLetter(c) && !char.IsDigit(c) && !char.IsPunctuation(c) && !char.IsWhiteSpace(c))
                {
                    retval.Add(sToken);
                    sToken = "";
                }
            }
            if (sToken != "")
            {
                retval.Add(sToken);
            }

            return retval;
        }
    }
}
