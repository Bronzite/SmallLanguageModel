using SmallLanguageModels.LanguageModels;
using SmallLanguageModels.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmallLanguageModelsTests
{
    public class MarkovChainTests
    {
        private string TestSentence;
        [SetUp]
        public void Setup()
        {
            TestSentence = "The quick brown fox jumped over the lazy dog.";
        }

        [Test]
        public void TrainOnSentenceSingleCase()
        {
            MarkovChain<string> markovChain = new MarkovChain<string>();
            List<string> tokens = (new UTF8Tokenizer()).GetTokens(TestSentence.ToUpper());
            markovChain.Train(tokens);

            Assert.That("THE", Is.EqualTo(markovChain.GetTokenValue(1)));
            Assert.That(" ", Is.EqualTo(markovChain.GetTokenValue(2)));
            Assert.That("QUICK", Is.EqualTo(markovChain.GetTokenValue(3)));
            Assert.That("BROWN", Is.EqualTo(markovChain.GetTokenValue(4)));
            Assert.That("FOX", Is.EqualTo(markovChain.GetTokenValue(5)));
            Assert.That("JUMPED", Is.EqualTo(markovChain.GetTokenValue(6)));
            Assert.That("OVER", Is.EqualTo(markovChain.GetTokenValue(7)));
            Assert.That("LAZY", Is.EqualTo(markovChain.GetTokenValue(8)));
            Assert.That("DOG", Is.EqualTo(markovChain.GetTokenValue(9)));
            Assert.That(".", Is.EqualTo(markovChain.GetTokenValue(10)));

            Assert.Pass();
        }

    }
}
