using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmallLanguageModels.LanguageModels
{
    public class MarkovChain<T> : ILanguageModel
    {
        public Dictionary<ulong,T> TokenList { get; set; }
        public Dictionary<ulong,Dictionary<ulong,ulong>> TokenTransitions { get; set; }
        public Dictionary<T,ulong> TokenLookup { get; set; }

        public MarkovChain()
        {
            TokenList = new Dictionary<ulong, T>();
            TokenTransitions = new Dictionary<ulong, Dictionary<ulong, ulong>>();
            TokenLookup = new Dictionary<T, ulong>();
            //Add start-of-message token.
            TokenTransitions.Add(0, new Dictionary<ulong, ulong>());
            //Add end-of-message token.
            TokenTransitions.Add(ulong.MaxValue, new Dictionary<ulong, ulong>());
            Random = new Random();
        }

        public Random Random { get; set; }

        public ulong AddTokenValue(object token)
        {
            ulong lTop = (ulong)TokenList.LongCount();
            TokenList.Add(lTop + 1, (T)token);
            TokenLookup.Add((T)token, lTop + 1);
            return lTop + 1;
        }

        public ulong AddTokenValue(T token)
        {
            return AddTokenValue((object)token);
        }

        public void DeserializeModel(Stream stream)
        {
            TokenList = JsonSerializer.Deserialize<Dictionary<ulong,T>>(stream);
        }

        public List<KeyValuePair<ulong, object>> GetTokenTable()
        {
            List<KeyValuePair<ulong, object>> retval = new List<KeyValuePair<ulong, object>>();
            foreach(ulong l in TokenList.Keys)
            {
                retval.Add(new KeyValuePair<ulong, object>(l, TokenList[l]));
            }
            return retval;
        }

        public object GetTokenValue(ulong token)
        {
            return TokenList[token];    
        }

        public ulong NextToken(ulong[] tokens)
        {
            ulong uOtherLong = 0;
            if(tokens.Length > 0) uOtherLong = tokens[tokens.Length - 1];
            int iLength = 0;
            foreach(ulong uKeys in TokenTransitions[uOtherLong].Keys)
            {
                iLength += (int)TokenTransitions[uOtherLong][uKeys];
            }
            int iRandom = Random.Next(iLength);
            for(int i = 0; i < TokenTransitions[uOtherLong].Keys.Count; i++)
            {
                ulong uKey = TokenTransitions[uOtherLong].Keys.ElementAt(i);
                if(iRandom < (int)TokenTransitions[uOtherLong][uKey])
                {
                    return uKey;
                }
                iRandom -= (int)TokenTransitions[uOtherLong][uKey];
            }

            return ulong.MaxValue;
        }

        

        public void SerializeModel(Stream stream)
        {
            JsonSerializer.Serialize<Dictionary<ulong, T>>(stream,TokenList);
        }

        public void Train(IEnumerable<object> values)
        {
            ulong lLastTokenId = 0;
            ulong lCurrentTokenId = 0;
            foreach(object o in values)
            {
                if (o is T)
                {
                    T token = (T)o;
                    if (!TokenLookup.ContainsKey(token))
                    {
                        lCurrentTokenId = AddTokenValue(token);
                    }
                    else
                    {
                        lCurrentTokenId = TokenLookup[token];
                    }
                    if (!TokenTransitions.ContainsKey(lCurrentTokenId))
                    {
                        TokenTransitions.Add(lCurrentTokenId, new Dictionary<ulong, ulong>());
                    }
                    if (!TokenTransitions[lLastTokenId].ContainsKey(lCurrentTokenId))
                    {
                        TokenTransitions[lLastTokenId].Add(lCurrentTokenId, 0);
                    }

                    TokenTransitions[lLastTokenId][lCurrentTokenId]++;
                    lLastTokenId = lCurrentTokenId;
                }

            }
            if (!TokenTransitions[lLastTokenId].ContainsKey(ulong.MaxValue)) TokenTransitions[lLastTokenId].Add(ulong.MaxValue, 0);
            TokenTransitions[lLastTokenId][ulong.MaxValue]++;
        }
    }
}
